using Application.Common;
using Application.Todos.Http.Response;
using Core.Entities;
using MediatR;

namespace Application.Todos.Command.GetAllTodo;

public class GetAllTodoCommand : IRequest<CustomResult<List<TodoHttpResponse>>>
{

}