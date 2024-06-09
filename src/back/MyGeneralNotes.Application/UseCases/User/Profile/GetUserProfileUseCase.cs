using AutoMapper;
using MyGeneralNotes.Communication.Responses;
using MyGeneralNotes.Domain.Services.LoggedUser;

namespace MyGeneralNotes.Application.UseCases.User.Profile;
public class GetUserProfileUseCase(ILoggedUser logged, IMapper mapper) : IGetUserProfileUseCase
{
    private readonly ILoggedUser _logged = logged;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseUserProfile> Execute()
    {
        var user = await _logged.User();

        return _mapper.Map<ResponseUserProfile>(user);

    }
}
