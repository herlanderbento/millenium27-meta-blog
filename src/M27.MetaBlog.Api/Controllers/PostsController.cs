using MediatR;
using Microsoft.AspNetCore.Mvc;
using M27.MetaBlog.Api.ApiModels.Post;
using M27.MetaBlog.Api.Presenters;
using M27.MetaBlog.Api.Validators;
using M27.MetaBlog.Application.UseCases.Post.Common;
using M27.MetaBlog.Application.UseCases.Post.DeletePost;
using M27.MetaBlog.Application.UseCases.Post.GetPost;
using M27.MetaBlog.Application.UseCases.Post.GetPostBySlug;
using M27.MetaBlog.Application.UseCases.Post.ListPosts;
using M27.MetaBlog.Domain.Shared.SearchableRepository;
using Microsoft.AspNetCore.Authorization;

namespace M27.MetaBlog.Api.Controllers;

[ApiController]
[Route("/api/posts")]
public class PostsController(IMediator mediator, RequestValidator requestValidator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly RequestValidator _requestValidator = requestValidator;
    
    [HttpPost]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ApiPresenter<PostOutput>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(
        [FromForm] CreatePostApiInput request,
        CancellationToken cancellationToken
    )
    {
        var input = request.ToCreatePostInput();
        var output = await _mediator.Send(input, cancellationToken);
        return CreatedAtAction(
            nameof(Create), 
            new { output.Id }, 
            new ApiPresenter<PostOutput>(output));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ListPostsOutput), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        CancellationToken cancellationToken,        
        [FromQuery] int? page = null,
        [FromQuery(Name = "per_page")] int? perPage = null,
        [FromQuery] string? search = null,
        [FromQuery] string? sort = null,
        [FromQuery] SearchOrder? dir = null
    )
    {
        var input = new ListPostsInput();
        if (page is not null) input.Page = page.Value;
        if (perPage is not null) input.PerPage = perPage.Value;
        if (!String.IsNullOrWhiteSpace(search)) input.Search = search;
        if (!String.IsNullOrWhiteSpace(sort)) input.Sort = sort;
        if (dir is not null) input.Dir = dir.Value;
        
        var output = await _mediator.Send(input, cancellationToken);
        return Ok(
            new ApiPresenterList<PostOutput>(output)
        );
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PostOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(new GetPostInput(id), cancellationToken);
        return Ok(new ApiPresenter<PostOutput>(output));
    }
    
    [HttpGet("slug/{slug}")]
    [ProducesResponseType(typeof(PostOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBySlug(
        [FromRoute] string slug,
        CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(new GetPostBySlugInput(slug), cancellationToken);
        return Ok(new ApiPresenter<PostOutput>(output));
    }

    [HttpPatch("{id:guid}")]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ApiPresenter<PostOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdatePostApiInput request,
        CancellationToken cancellationToken
        )
    {
        var input = request.ToUpdatePostInput(id);
        var output = await _mediator.Send(input, cancellationToken);
        return Ok(new ApiPresenter<PostOutput>(output));
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _mediator.Send(new DeletePostInput(id), cancellationToken);
        return NoContent();
    }

}