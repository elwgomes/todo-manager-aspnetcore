using Application.Todos.Command.CreateTodo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Todos;

[ApiController]
[Route("api/todos")]
public class TodoController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateTodo(CreateTodoCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}