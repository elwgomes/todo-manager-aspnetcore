using Application.Common;
using Core.Entities;
using MediatR;

namespace Application.Security.Token.Command.GetClaims;

public class GetClaimsCommand : IRequest<IDictionary<string, string>>
{
    public string Token { get; set; }
}