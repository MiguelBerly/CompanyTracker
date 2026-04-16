using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using CompanyTracker.Api.Domain;
using CompanyTracker.Api.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace CompanyTracker.Api.Data.Repositories;

public class TokenRepository : ITokenRepository
{
    private static readonly RSA Rsa = RSA.Create(2048);

    private readonly RsaSecurityKey _privateKey = new (Rsa)
    {
        KeyId = "CompanyTrackerKey"
    };
    
    public string CreateTestToken(TokenType tokenType)
    {
        var role = tokenType switch
        {
            TokenType.Member => "member",
            TokenType.Admin => "admin",
            _ => throw new ArgumentOutOfRangeException(nameof(tokenType), tokenType, null)
        };

        var claims = new List<Claim>
        {
            new("role", role)
        };
    
        var signingCredentials = new SigningCredentials(_privateKey, SecurityAlgorithms.RsaSha256);

        var now  = DateTime.UtcNow;
    
        var token = new JwtSecurityToken(
            issuer: "http://localhost:5400",
            audience: "http://localhost:5400",
            claims: claims,
            expires: now.AddMinutes(30),
            signingCredentials: signingCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
    
        return tokenString;
    }

    public JsonWebKey PublishKey()
    {
        var publicKey = new RsaSecurityKey(Rsa.ExportParameters(false))
        {
            KeyId = "CompanyTrackerKey"
        };
 
        var jwk = JsonWebKeyConverter.ConvertFromRSASecurityKey(publicKey);
        
        return jwk;
    }
}
