using Application.Common;
using Core.Entities;
using MediatR;

namespace Application.Security.Login.Command;

public class LoginCommand : IRequest<CustomResult<string>>
{
    public string Username { get; set; }
    public string Password { get; set; }
    
    public User ToEntity() => new(Username, Password);
    
}