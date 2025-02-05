using M27.MetaBlog.Api.ApiModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using M27.MetaBlog.Api.Presenters;
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
public class UsersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    
    [HttpPost]
    [ProducesResponseType(typeof(ApiPresenter<UserOutput>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserInput input,
        CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(input, cancellationToken);
        return CreatedAtAction(
            nameof(Create),
            new { output.Id },
            new ApiPresenter<UserOutput>(output)
        );
    }
    
    [HttpGet]
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
        [FromBody] UpdateUserApiInput apiInput,
        CancellationToken cancellationToken
    )
    {
        var input = new UpdateUserInput(
            id, 
            apiInput.Name, 
            apiInput.Email, 
            apiInput.Password,
            apiInput.Role, 
            apiInput.IsActive
        );
        
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