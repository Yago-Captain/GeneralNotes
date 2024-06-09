using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Communication.Response;

namespace MyGeneralNotes.Application.UseCases.User.Register;
public interface IRegisterUserUseCase
{
    public Task<ResponseRegisteredUser> Execute(RequestRegisteredUser request);
}
