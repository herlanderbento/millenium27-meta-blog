using M27.MetaBlog.Api.ApiModels.Category;
using M27.MetaBlog.Api.Presenters;
using M27.MetaBlog.Api.Validators;
using M27.MetaBlog.Application.UseCases.Category.Common;
using M27.MetaBlog.Application.UseCases.Category.CreateCategory;
using M27.MetaBlog.Application.UseCases.Category.DeleteCategory;
using M27.MetaBlog.Application.UseCases.Category.GetCategory;
using M27.MetaBlog.Application.UseCases.Category.ListCategories;
using M27.MetaBlog.Application.UseCases.Category.UpdateCategory;
using M27.MetaBlog.Domain.Shared.SearchableRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace M27.MetaBlog.Api.Controllers;

[ApiController]
[Route("/api/categories")]
public class CategoriesController(IMediator mediator, RequestValidator requestValidator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly RequestValidator _requestValidator = requestValidator;
    
    [HttpPost]
    [ProducesResponseType(typeof(ApiPresenter<CategoryOutput>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(
        [FromBody] CreateCategoryInput request,
        CancellationToken cancellationToken
    )
    {
        _requestValidator.Validate(request, cancellationToken);
        
        var output = await _mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(Create), new { output.Id }, new ApiPresenter<CategoryOutput>(output));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ListCategoriesOutput), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        CancellationToken cancellationToken,        
        [FromQuery] int? page = null,
        [FromQuery(Name = "per_page")] int? perPage = null,
        [FromQuery] string? search = null,
        [FromQuery] string? sort = null,
        [FromQuery] SearchOrder? dir = null
    )
    {
        var input = new ListCategoriesInput();
        if (page is not null) input.Page = page.Value;
        if (perPage is not null) input.PerPage = perPage.Value;
        if (!String.IsNullOrWhiteSpace(search)) input.Search = search;
        if (!String.IsNullOrWhiteSpace(sort)) input.Sort = sort;
        if (dir is not null) input.Dir = dir.Value;
        
        var output = await _mediator.Send(input, cancellationToken);
        return Ok(new ApiPresenterList<CategoryOutput>(output));
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiPresenter<CategoryOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(new GetCategoryInput(id), cancellationToken);
        return Ok(new ApiPresenter<CategoryOutput>(output));
    }
    
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(typeof(ApiPresenter<CategoryOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateCategoryApiInput request,
        CancellationToken cancellationToken
    )
    {
        var input = new UpdateCategoryInput(
            id, 
            request.Name, 
            request.Description, 
            request.IsActive
        );
        
        _requestValidator.Validate(input, cancellationToken);
        
        var output = await _mediator.Send(input, cancellationToken);
        return Ok(new ApiPresenter<CategoryOutput>(output));
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _mediator.Send(new DeleteCategoryInput(id), cancellationToken);
        return NoContent();
    }

}