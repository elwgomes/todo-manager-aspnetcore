using Application.Common;
using MediatR;

namespace Application.Todos.Command.DeleteTodo;

public class DeleteTodoCommand : IRequest<CustomResult<string>>
{
    public Guid Id { get; }

    public DeleteTodoCommand(Guid id)
    {
        Id = id;
    }
}