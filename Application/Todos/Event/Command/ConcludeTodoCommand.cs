using Application.Common;
using Application.Todos.Http.Response;
using MediatR;

namespace Application.Todos.Event.Command;

public class ConcludeTodoCommand : IRequest<CustomResult<TodoHttpResponse>>
{
    public Guid Id { get; set; }

    public ConcludeTodoCommand(Guid id)
    {
        Id = id;
    }
}