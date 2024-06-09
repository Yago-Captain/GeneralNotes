using Microsoft.EntityFrameworkCore;
using MyGeneralNotes.Domain.Entities;
using MyGeneralNotes.Domain.Repositories.User;

namespace MyGeneralNotes.Infrastructure.DataAccess.Repositories;
public class UserRepository(MyGeneralNotesDbContext context) : IUserWriteOnlyRepository, IUserReadOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly MyGeneralNotesDbContext _context = context;

    public async Task Add(User user)
        => await _context.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) =>
        await _context.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);

    public async Task<User?> GetUserByEmailAndPassword(string email, string password)
    {
        return await _context
            .Users.AsNoTracking().FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email) && user.Password.Equals(password));
    }
    public async Task<bool> ExistActiveUserWitchIdentifier(Guid userIdentifier)
        => await _context.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.Active);

    public async Task<User> GetByUserIdentifier(Guid userIdentifier)
    {
        return await _context
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.Active && user.UserIdentifier.Equals(userIdentifier));
    }

    public async Task<User> GetById(long id)
    {
        return await _context
            .Users.FirstAsync(user => user.Id == id);
    }

    public void Update(User user) => _context.Users.Update(user);
}
