using Application.Common;
using Application.Common.Interfaces;
using Application.Security.Token.Command.GetClaims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Todos.Command.DeleteTodo;

public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, CustomResult<string>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public DeleteTodoHandler(IApplicationDbContext context, IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<CustomResult<string>> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        
        var claims = await _mediator.Send((new GetClaimsCommand()));
        var userId = Guid.Parse(claims["id"]);
        
        var obj = await _context.Todos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        
        if (obj == null || obj.UserId != userId) throw new Exception();
        
        _context.Todos.Remove(obj);
        await _context.SaveChangesAsync(cancellationToken);
        return new CustomResult<string>(204, "no content", "Todo has been deleted.");
    }
}