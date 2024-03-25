using Application.Common;
using Core.Entities;
using MediatR;

namespace Application.Todos.Command.CreateTodo;

public class CreateTodoCommand : IRequest<CustomResult<Todo>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    
    public Todo ToEntity() => new(Title, Description, DateTime.Now, UserId);
    
}