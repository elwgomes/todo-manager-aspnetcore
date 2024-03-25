using Application.Common;
using Application.Common.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Command.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, CustomResult<User>>
{
    
    private readonly IApplicationDbContext _context;
    
    public CreateUserHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<CustomResult<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validateUsername = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        
        if (validateUsername != null) throw new Exception();
        
        request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        var user = request.ToEntity();
        
        await _context.Users.AddAsync(user, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return new CustomResult<Core.Entities.User>(200, "success", "User has been created.", null);
    }
}