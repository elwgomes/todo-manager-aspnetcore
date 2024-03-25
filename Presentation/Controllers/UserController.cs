using Application.Users.Command.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{

    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult> CreateUser(CreateUserCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    
}