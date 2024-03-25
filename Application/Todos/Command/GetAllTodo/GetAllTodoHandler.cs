using Application.Common;
using Application.Common.Interfaces;
using Application.Security.Token.Command.GetClaims;
using Application.Todos.Http.Response;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Todos.Command.GetAllTodo;

public class GetAllTodoHandler : IRequestHandler<GetAllTodoCommand, CustomResult<List<TodoHttpResponse>>>
{

    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public GetAllTodoHandler(IApplicationDbContext context, IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<CustomResult<List<TodoHttpResponse>>> Handle(GetAllTodoCommand request, CancellationToken cancellationToken)
    {
        var claims = await _mediator.Send((new GetClaimsCommand()));
        
        var userId = Guid.Parse(claims["id"]);
        
        var todos = await _context.Todos.Where(t => t.UserId == userId).ToListAsync();
        
        var todoResponses = new List<TodoHttpResponse>();

        foreach (var todo in todos)
        {
            var todoResponse = new TodoHttpResponse(todo.Id, todo.Title, todo.Description, todo.CreatedAt, todo.ConcludedAt);
            todoResponses.Add(todoResponse);
        }
        
        return new CustomResult<List<TodoHttpResponse>>(200, "success", null, todoResponses);
    }
}