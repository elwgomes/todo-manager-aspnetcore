using Application.Common;
using Application.Common.Interfaces;
using Application.Security.Token.GenerateToken;
using Core.Users.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Security.Login.Command;

public class LoginHandler : IRequestHandler<LoginCommand, CustomResult<string>>
{
    
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    
    public LoginHandler(IApplicationDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    
    public async Task<CustomResult<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        
        if (user == null) throw new CustomException();
        
        // decode and check password
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password)) throw new CustomException();

        user.LastLogin = DateTime.Now;
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        
        var http = new GenerateTokenCommand()
        {
            Id = user.Id,
            Username = user.Username
        };

        var token = await _mediator.Send(http);
        
        return new CustomResult<string>(200, "success", "Login successfull.", token.Data);
    }
}