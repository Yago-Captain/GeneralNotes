using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Communication.Response;

namespace MyGeneralNotes.Application.UseCases.Login.DoLogin;
public interface IDoLoginUseCase
{
    public Task<ResponseRegisteredUser> Execute(RequestLogin request);
}
