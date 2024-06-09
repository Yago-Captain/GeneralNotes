using MyGeneralNotes.Domain.Entities;

namespace MyGeneralNotes.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    public Task<User> User();
}
