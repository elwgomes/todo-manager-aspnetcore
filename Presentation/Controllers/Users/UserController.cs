using Application.Common.Exceptions;
using Application.Users.Command.CreateUser;
using Core.Users.Exceptions;
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
        try
        {
            return Ok(await _mediator.Send(command));
        }         
        catch (CustomException)
        {
            return BadRequest(new ExceptionDetails(400, "BAD REQUEST", "Duplicated user..."));
        }
    }
    
}