using Application.Common;
using Application.Common.Interfaces;
using Application.Security.Token.Command.GetClaims;
using Application.Todos.Http.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Todos.Command.GetTodoById;

public class GetTodoByIdHandler : IRequestHandler<GetTodoByIdCommand, CustomResult<TodoHttpResponse>>
{
 
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public GetTodoByIdHandler(IApplicationDbContext context, IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CustomResult<TodoHttpResponse>> Handle(GetTodoByIdCommand request, CancellationToken cancellationToken)
    {
        var claims = await _mediator.Send((new GetClaimsCommand()));
        var userId = Guid.Parse(claims["id"]);
        
        var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == request.Id);
        
        if (todo.UserId != userId) throw new Exception();
        
        var response = new TodoHttpResponse(
            todo.Title,
            todo.Description,
            todo.CreatedAt,
            todo.ConcludedAt
            );
        
        return new CustomResult<TodoHttpResponse>(200, "success", null, response);
    }
}