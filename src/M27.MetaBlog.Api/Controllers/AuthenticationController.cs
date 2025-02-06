using M27.MetaBlog.Api.Presenters;
using M27.MetaBlog.Application.UseCases.User.Authenticate;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace M27.MetaBlog.Api.Controllers;

[ApiController]
[Route("/api/authentication")]
public class AuthenticationController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(ApiPresenter<AuthenticateOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Authenticate(
        [FromBody] AuthenticateInput request,
        CancellationToken cancellationToken
        )
    {
        var input = new AuthenticateInput(
            request.Email,
            request.Password
        );
        var output = await _mediator.Send(input, cancellationToken);
        return Ok(new ApiPresenter<AuthenticateOutput>(output));
    }
}