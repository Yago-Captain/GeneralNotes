using AutoMapper;
using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Communication.Responses;
using MyGeneralNotes.Domain.Repositories;
using MyGeneralNotes.Domain.Repositories.Routine;
using MyGeneralNotes.Domain.Services.LoggedUser;
using MyGeneralNotes.Exceptions.ExceptionsBase;

namespace MyGeneralNotes.Application.UseCases.Routines.Register;
public class RegisterRoutineUseCase(IUnitOfWork unitOfWork, IMapper mapper, ILoggedUser logged, IRoutineWriteOnlyRepository repository) : IRegisterRoutineUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ILoggedUser _logged = logged;
    private readonly IRoutineWriteOnlyRepository _repository = repository;

    public async Task<ResponseRoutine> Execute(RequestRoutine request)
    {
        Validate(request);

        var loggedUser = await _logged.User();

        var routine = _mapper.Map<Domain.Entities.Routine>(request);
        routine.UserId = loggedUser.Id;

        await _repository.Register(routine);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRoutine>(routine);
    }

    private static void Validate(RequestRoutine request)
    {
        var validator = new RegisterRoutineValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMenssages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMenssages);
        }
    }
}
