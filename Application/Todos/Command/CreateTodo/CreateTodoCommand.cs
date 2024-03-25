using Application.Common;
using Application.Security.Token.Command.GetClaims;
using Core.Entities;
using MediatR;

namespace Application.Todos.Command.CreateTodo;

public class CreateTodoCommand : IRequest<CustomResult<Todo>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    
}