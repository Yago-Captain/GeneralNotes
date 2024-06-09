using MyGeneralNotes.Communication.Requests;

namespace MyGeneralNotes.Application.UseCases.User.ChangePassword;
public interface IChangePasswordUseCase
{
    public Task Execute(RequestChangePassword request);
}
