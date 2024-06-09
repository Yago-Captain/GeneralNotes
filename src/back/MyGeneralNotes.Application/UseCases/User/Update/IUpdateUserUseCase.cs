using MyGeneralNotes.Communication.Requests;

namespace MyGeneralNotes.Application.UseCases.User.Update;
public interface IUpdateUserUseCase
{
    public Task Execute(RequestUpdateUser request);
}
