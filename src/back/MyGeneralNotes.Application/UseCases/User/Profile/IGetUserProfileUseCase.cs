using MyGeneralNotes.Communication.Responses;

namespace MyGeneralNotes.Application.UseCases.User.Profile;
public interface IGetUserProfileUseCase
{
    public Task<ResponseUserProfile> Execute();
}
