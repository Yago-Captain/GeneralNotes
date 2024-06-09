using Microsoft.IdentityModel.Tokens;
using MyGeneralNotes.Domain.Security.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyGeneralNotes.Infrastructure.Security.Tokens.Acces.Generator;
public class JwtTokenGenerator : JwtTokenHandler, IAccessTokenGenerator
{
    private readonly uint _tokenLifeTime;
    private readonly string _signinKey;

    public JwtTokenGenerator(uint tokenLifeTime, string signinKey)
    {
        _tokenLifeTime = tokenLifeTime;
        _signinKey = signinKey;
    }

    public string Generate(Guid userIdentifier)
    {
        var clains = new List<Claim>()
        {
            new Claim(ClaimTypes.Sid, userIdentifier.ToString())
        };

        var tokenDescripter = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(clains),
            Expires = DateTime.UtcNow.AddMinutes(_tokenLifeTime),
            SigningCredentials = new SigningCredentials(SecurityKey(_signinKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescripter);

        return tokenHandler.WriteToken(securityToken);
    }
}
