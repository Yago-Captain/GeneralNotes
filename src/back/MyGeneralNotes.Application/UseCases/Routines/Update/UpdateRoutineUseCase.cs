using AutoMapper;
using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Domain.Entities;
using MyGeneralNotes.Domain.Repositories;
using MyGeneralNotes.Domain.Repositories.Routine;
using MyGeneralNotes.Domain.Services.LoggedUser;
using MyGeneralNotes.Exceptions;
using MyGeneralNotes.Exceptions.ExceptionsBase;

namespace MyGeneralNotes.Application.UseCases.Routines.Update;
public class UpdateRoutineUseCase(IMapper mapper, IRoutineUpdateOnlyRepository repository, ILoggedUser logged, IUnitOfWork unitOfWork) : IUpdateRoutineUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IRoutineUpdateOnlyRepository _repository = repository;
    private readonly ILoggedUser _logged = logged;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Update(long routineId, RequestRoutine request)
    {
        var loggedUser = await _logged.User();
        var routine = await _repository.GetById(routineId);

        Validate(loggedUser, routine, request);

        _mapper.Map(request, routine);

        _repository.Update(routine!);

        await _unitOfWork.Commit();
    }

    private static void Validate(Domain.Entities.User loggedUser, Routine? routine, RequestRoutine request)
    {
        if (routine is null || routine.UserId != loggedUser.Id)
            throw new ErrorOnValidationException([MessagesException.ROUTINE_NOT_FOUND]);

        var validator = new UpdateRoutineValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMenssages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMenssages);
        }
    }
}
