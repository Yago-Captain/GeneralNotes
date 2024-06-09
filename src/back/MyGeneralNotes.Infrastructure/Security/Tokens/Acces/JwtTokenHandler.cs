using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MyGeneralNotes.Infrastructure.Security.Tokens.Acces;
public abstract class JwtTokenHandler
{
    protected static SymmetricSecurityKey SecurityKey(string sigininKey)
    {
        var bytes = Encoding.UTF8.GetBytes(sigininKey);

        return new SymmetricSecurityKey(bytes);
    }
}
