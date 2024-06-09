using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Communication.Responses;

namespace MyGeneralNotes.Application.UseCases.Routines.Register;
public interface IRegisterRoutineUseCase
{
    Task<ResponseRoutine> Execute(RequestRoutine request);
}