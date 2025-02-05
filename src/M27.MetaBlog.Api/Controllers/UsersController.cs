﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using M27.MetaBlog.Api.Presenters;
using M27.MetaBlog.Application.UseCases.User.Common;
using M27.MetaBlog.Application.UseCases.User.CreateUser;

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
}