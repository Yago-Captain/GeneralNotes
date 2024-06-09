namespace MyGeneralNotes.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    public Task<bool> ExistActiveUserWithEmail(string email);
    public Task<Entities.User?> GetUserByEmailAndPassword(string email, string password);
    public Task<bool> ExistActiveUserWitchIdentifier(Guid userIdentifier);
    public Task<Entities.User> GetByUserIdentifier(Guid userIdentifier);

}
