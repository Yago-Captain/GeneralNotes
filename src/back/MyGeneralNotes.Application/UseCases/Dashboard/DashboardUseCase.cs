using AutoMapper;
using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Communication.Responses;
using MyGeneralNotes.Domain.Repositories.Routine;
using MyGeneralNotes.Domain.Services.LoggedUser;

namespace MyGeneralNotes.Application.UseCases.Dashboard;
public class DashboardUseCase(IMapper mapper, IRoutineReadOnlyRepository repositoy, ILoggedUser logged) : IDashboardUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IRoutineReadOnlyRepository _repositoy = repositoy;
    private readonly ILoggedUser _logged = logged;

    public async Task<ResponseDashboard> GetDashboard(RequestDashboard request)
    {
        var loggedUser = await _logged.User();
        var routines = await _repositoy.GetAllUserRoutines(loggedUser.Id);

        return new ResponseDashboard
        {
            Routines = _mapper.Map<List<ResponseRoutinesDashboard>>(routines)
        };
    }
}
