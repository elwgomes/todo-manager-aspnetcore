using System.Security.Claims;
using Application.Common;
using Application.Common.Interfaces;
using Application.Security.Token.Command.GetClaims;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Todos.Command.CreateTodo;

public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, CustomResult<Todo>>
{

    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CreateTodoHandler(IApplicationDbContext context, IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<CustomResult<Todo>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        
        var claims = await _mediator.Send((new GetClaimsCommand()));

        var user = await _context.Users.FindAsync(Guid.Parse(claims["id"]));
            
        if (user == null) return new CustomResult<Todo>(400, "error", "User not found.", null);
        
        var todo = new Todo
        {
            Title = request.Title,
            Description = request.Description,
            CreatedAt = DateTime.Now,
            UserId = Guid.Parse(claims["id"]),
            User = user
        };
        
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync(cancellationToken);
        
        return new CustomResult<Todo>(200, "success", "Todo has been created.", null);
    }
}