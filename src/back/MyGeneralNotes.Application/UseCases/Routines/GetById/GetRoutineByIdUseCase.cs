using AutoMapper;
using MyGeneralNotes.Communication.Responses;
using MyGeneralNotes.Domain.Entities;
using MyGeneralNotes.Domain.Repositories.Routine;
using MyGeneralNotes.Domain.Services.LoggedUser;
using MyGeneralNotes.Exceptions;
using MyGeneralNotes.Exceptions.ExceptionsBase;

namespace MyGeneralNotes.Application.UseCases.Routines.GetById;
public class GetRoutineByIdUseCase(IMapper mapper, IRoutineReadOnlyRepository repositoy, ILoggedUser logged) : IGetRoutineByIdUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IRoutineReadOnlyRepository _repositoy = repositoy;
    private readonly ILoggedUser _logged = logged;

    public async Task<ResponseRoutine> GetRoutineById(long routineId)
    {
        var loggedUser = await _logged.User();
        var routine = await _repositoy.GetRoutine(routineId);

        Validate(loggedUser, routine);

        return _mapper.Map<ResponseRoutine>(routine);
    }

    private static void Validate(Domain.Entities.User loggedUser, Routine? routine)
    {
        if (routine is null || routine.UserId != loggedUser.Id)
            throw new ErrorOnValidationException([MessagesException.ROUTINE_NOT_FOUND]);
    }
}
