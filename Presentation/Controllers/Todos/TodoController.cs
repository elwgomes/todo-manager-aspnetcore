using Application.Todos.Command.CreateTodo;
using Application.Todos.Command.DeleteTodo;
using Application.Todos.Command.GetAllTodo;
using Application.Todos.Command.GetTodoById;
using Application.Todos.Command.UpdateTodo;
using Application.Todos.Event.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Todos;

[Authorize]
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
    
    [HttpGet]
    public async Task<ActionResult> GetAllTodo()
    {
        var command = new GetAllTodoCommand();
        return Ok(await _mediator.Send(command));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetTodoById([FromRoute] Guid id)
    {
        var command = new GetTodoByIdCommand(id);
        return Ok(await _mediator.Send(command));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTodo([FromRoute] Guid id)
    {
        var command = new DeleteTodoCommand(id);
        return Ok(await _mediator.Send(command));
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTodo(UpdateTodoCommand command, [FromRoute] Guid id)
    {
        command.Id = id;
        return Ok(await _mediator.Send(command));
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult> ConcludeTodo([FromRoute] Guid id)
    {
        var command = new ConcludeTodoCommand(id);
        return Ok(await _mediator.Send(command));
    }
    
    
}