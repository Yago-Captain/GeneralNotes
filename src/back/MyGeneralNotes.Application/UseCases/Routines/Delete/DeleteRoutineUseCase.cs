
using MyGeneralNotes.Domain.Entities;
using MyGeneralNotes.Domain.Repositories;
using MyGeneralNotes.Domain.Repositories.Routine;
using MyGeneralNotes.Domain.Services.LoggedUser;
using MyGeneralNotes.Exceptions;
using MyGeneralNotes.Exceptions.ExceptionsBase;

namespace MyGeneralNotes.Application.UseCases.Routines.Delete;
public class DeleteRoutineUseCase(IRoutineReadOnlyRepository readOnlyRepository, IRoutineWriteOnlyRepository writeOnlyRepository, ILoggedUser logged, IUnitOfWork unitOfWork) : IDeleteRoutineUseCase
{
    private readonly IRoutineReadOnlyRepository _readOnlyRepository = readOnlyRepository;
    private readonly IRoutineWriteOnlyRepository _writeOnlyRepository = writeOnlyRepository;
    private readonly ILoggedUser _logged = logged;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Delete(long routineId)
    {
        var loggedUser = await _logged.User();

        var routine = await _readOnlyRepository.GetRoutine(routineId);

        Validate(loggedUser, routine);

        await _writeOnlyRepository.Delete(routineId);

        await _unitOfWork.Commit();
    }

    private static void Validate(Domain.Entities.User loggedUser, Routine? routine)
    {
        if (routine is null || routine.UserId != loggedUser.Id)
            throw new ErrorOnValidationException([MessagesException.ROUTINE_NOT_FOUND]);
    }
}
