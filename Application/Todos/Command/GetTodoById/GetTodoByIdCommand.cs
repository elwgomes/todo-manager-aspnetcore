using Application.Common;
using Application.Todos.Http.Response;
using MediatR;

namespace Application.Todos.Command.GetTodoById;

public class GetTodoByIdCommand : IRequest<CustomResult<TodoHttpResponse>>
{
    public Guid Id { get; }

    public GetTodoByIdCommand(Guid id)
    {
        Id = id;
    }
}