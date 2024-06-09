using MyGeneralNotes.Communication.Responses;

namespace MyGeneralNotes.Application.UseCases.Routines.GetById;
public interface IGetRoutineByIdUseCase
{
    Task<ResponseRoutine> GetRoutineById(long routineId);
}
