using Application.Common;
using Application.Todos.Http.Response;
using MediatR;

namespace Application.Todos.Command.UpdateTodo;

public class UpdateTodoCommand : IRequest<CustomResult<TodoHttpResponse>>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public UpdateTodoCommand()
    {
        
    }
    
    public UpdateTodoCommand(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public UpdateTodoCommand(Guid id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
    }
}