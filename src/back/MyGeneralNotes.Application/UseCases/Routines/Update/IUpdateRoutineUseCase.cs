using MyGeneralNotes.Communication.Requests;

namespace MyGeneralNotes.Application.UseCases.Routines.Update;
public interface IUpdateRoutineUseCase
{
    Task Update(long routineId, RequestRoutine request);
}
