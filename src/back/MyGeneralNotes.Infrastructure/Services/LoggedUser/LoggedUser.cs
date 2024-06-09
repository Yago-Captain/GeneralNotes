using Microsoft.EntityFrameworkCore;
using MyGeneralNotes.Domain.Entities;
using MyGeneralNotes.Domain.Security.Tokens;
using MyGeneralNotes.Domain.Services.LoggedUser;
using MyGeneralNotes.Infrastructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyGeneralNotes.Infrastructure.Services.LoggedUser;
public class LoggedUser(MyGeneralNotesDbContext context, ITokenProvider tokenProvider) : ILoggedUser
{
    private readonly MyGeneralNotesDbContext _context = context;
    private readonly ITokenProvider _tokenProvider = tokenProvider;

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();
        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
        var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);

        return await _context
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.Active & user.UserIdentifier == userIdentifier);
    }
}