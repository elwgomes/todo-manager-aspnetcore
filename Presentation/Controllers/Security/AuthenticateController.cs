using Application.Security.Login.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Security;

[ApiController]
[Route("api/auth")]
public class AuthenticateController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    
}