using Application.Common;
using Core.Entities;
using MediatR;

namespace Application.Security.Token.GenerateToken;

public class GenerateTokenCommand : IRequest<CustomResult<string>>
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    
}