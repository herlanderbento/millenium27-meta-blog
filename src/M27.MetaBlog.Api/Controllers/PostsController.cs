using M27.MetaBlog.Api.Presenters;
using M27.MetaBlog.Api.Validators;
using M27.MetaBlog.Application.UseCases.Post.Common;
using M27.MetaBlog.Application.UseCases.Post.CreatePost;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace M27.MetaBlog.Api.Controllers;

[ApiController]
[Route("/api/posts")]
public class PostsController(IMediator mediator, RequestValidator requestValidator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly RequestValidator _requestValidator = requestValidator;
    
    [HttpPost]
    [ProducesResponseType(typeof(ApiPresenter<PostOutput>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(
        [FromBody] CreatePostInput request,
        CancellationToken cancellationToken
    )
    {
        //_requestValidator.Validate(request, cancellationToken);
        
        var output = await _mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(Create), new { output.Id }, new ApiPresenter<PostOutput>(output));
    }
}