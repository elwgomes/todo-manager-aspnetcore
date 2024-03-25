using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common;
using Application.Common.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Security.Token.GenerateToken;

public class GenerateTokenHandler : IRequestHandler<GenerateTokenCommand, CustomResult<string>>
{
    private readonly IApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    
    public GenerateTokenHandler(IApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    
    public async Task<CustomResult<string>> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
        
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim("id", request.Id.ToString()),
            new Claim("username", request.Username)
        };
        
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credential
        );
        
        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new CustomResult<string>(200, "success", "User validated.", tokenString);
    }
}