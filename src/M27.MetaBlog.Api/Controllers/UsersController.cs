using M27.MetaBlog.Api.ApiModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using M27.MetaBlog.Api.Presenters;
using M27.MetaBlog.Api.Validators;
using M27.MetaBlog.Application.UseCases.User.Common;
using M27.MetaBlog.Application.UseCases.User.CreateUser;
using M27.MetaBlog.Application.UseCases.User.DeleteUser;
using M27.MetaBlog.Application.UseCases.User.GetUser;
using M27.MetaBlog.Application.UseCases.User.ListUsers;
using M27.MetaBlog.Application.UseCases.User.UpdateUser;
using M27.MetaBlog.Domain.Shared.SearchableRepository;

namespace M27.MetaBlog.Api.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController(IMediator mediator, RequestValidator requestValidator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly RequestValidator _requestValidator = requestValidator;
    
    [HttpPost]
    [ProducesResponseType(typeof(ApiPresenter<UserOutput>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserInput request,
        CancellationToken cancellationToken
    )
    {
        _requestValidator.Validate(request, cancellationToken);
        
        var output = await _mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(Create), new { output.Id }, new ApiPresenter<UserOutput>(output));
    }
    

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ListUsersOutput), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        CancellationToken cancellationToken,        
        [FromQuery] int? page = null,
        [FromQuery(Name = "per_page")] int? perPage = null,
        [FromQuery] string? search = null,
        [FromQuery] string? sort = null,
        [FromQuery] SearchOrder? dir = null
    )
    {
        var input = new ListUsersInput();
        if (page is not null) input.Page = page.Value;
        if (perPage is not null) input.PerPage = perPage.Value;
        if (!String.IsNullOrWhiteSpace(search)) input.Search = search;
        if (!String.IsNullOrWhiteSpace(sort)) input.Sort = sort;
        if (dir is not null) input.Dir = dir.Value;
        
        var output = await _mediator.Send(input, cancellationToken);
        return Ok(
            new ApiPresenterList<UserOutput>(output)
        );
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiPresenter<UserOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(new GetUserInput(id), cancellationToken);
        return Ok(new ApiPresenter<UserOutput>(output));
    }

    [HttpPatch("{id:guid}")]
    [ProducesResponseType(typeof(ApiPresenter<UserOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateUserApiInput request,
        CancellationToken cancellationToken
    )
    {
        var input = new UpdateUserInput(
            id, 
            request.Name, 
            request.Email, 
            request.Password,
            request.Role, 
            request.IsActive
        );
        
        _requestValidator.Validate(input, cancellationToken);
        
        var output = await _mediator.Send(input, cancellationToken);
        return Ok(new ApiPresenter<UserOutput>(output));
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _mediator.Send(new DeleteUserInput(id), cancellationToken);
        return NoContent();
    }
    
}