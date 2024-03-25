using Application.Common;
using Application.Common.Interfaces;
using Application.Security.Token.Command.GetClaims;
using Application.Todos.Http.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Todos.Command.UpdateTodo;

public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand, CustomResult<TodoHttpResponse>>
{
    
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public UpdateTodoHandler(IApplicationDbContext context, IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<CustomResult<TodoHttpResponse>> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var claims = await _mediator.Send((new GetClaimsCommand()));
        var userId = Guid.Parse(claims["id"]);
        
        var obj = await _context.Todos.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        if (obj == null || obj.UserId != userId) throw new Exception();

        obj.Title = request.Title;
        obj.Description = request.Description;

        _context.Todos.Update(obj);
        await _context.SaveChangesAsync(cancellationToken);
        
        return new CustomResult<TodoHttpResponse>(200, "success", "Todo has been updated.");
    }
}