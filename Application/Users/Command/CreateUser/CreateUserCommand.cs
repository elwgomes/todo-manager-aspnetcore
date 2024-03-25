using Application.Common;
using Core.Entities;
using MediatR;

namespace Application.Users.Command.CreateUser;

public class CreateUserCommand : IRequest<CustomResult<User>>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime? CreatedAt { get; private set; }
    
    public User ToEntity() => new(Username, Password, DateTime.Now);
}