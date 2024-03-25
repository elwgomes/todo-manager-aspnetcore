using Application.Common;
using Application.Common.Interfaces;
using Core.Entities;
using MediatR;

namespace Application.Todos.Command.CreateTodo;

public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, CustomResult<Todo>>
{
    private readonly IApplicationDbContext _context;
    
    public CreateTodoHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<CustomResult<Todo>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = request.ToEntity();
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync(cancellationToken);
        
        return new CustomResult<Todo>(200, "success", "Todo has been created.", null);
    }
}