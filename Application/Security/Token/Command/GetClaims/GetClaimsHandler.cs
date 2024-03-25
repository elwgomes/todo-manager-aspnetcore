using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.Common;
using Application.Common.Interfaces;
using Application.Users.Command.CreateUser;
using Core.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Security.Token.Command.GetClaims;

public class GetClaimsHandler : IRequestHandler<GetClaimsCommand, IDictionary<string, string>>
{
    private readonly IApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public GetClaimsHandler(IApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public Task<IDictionary<string, string>> Handle(GetClaimsCommand request, CancellationToken cancellationToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _configuration["JwtSettings:Issuer"],
            ValidAudience = _configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]))
        };
        
        SecurityToken validatedToken;
        var principal = tokenHandler.ValidateToken(request.Token, validationParameters, out validatedToken);

        var claims = new Dictionary<string, string>();
        foreach (var claim in principal.Claims)
        {
            claims.Add(claim.Type, claim.Value);
        }

        return Task.FromResult<IDictionary<string, string>>(claims);
        
    }
}