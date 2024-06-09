using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Domain.Extensions;
using MyGeneralNotes.Domain.Repositories;
using MyGeneralNotes.Domain.Repositories.User;
using MyGeneralNotes.Domain.Services.LoggedUser;
using MyGeneralNotes.Exceptions;
using MyGeneralNotes.Exceptions.ExceptionsBase;

namespace MyGeneralNotes.Application.UseCases.User.Update;
public class UpdateUserUseCase(ILoggedUser logged, IUserUpdateOnlyRepository repository, IUserReadOnlyRepository userReadOnlyRepository, IUnitOfWork unitOfWork) : IUpdateUserUseCase
{
    private readonly ILoggedUser _logged = logged;
    private readonly IUserUpdateOnlyRepository _repository = repository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(RequestUpdateUser request)
    {
        var loggedUser = await _logged.User();

        await Validate(request, loggedUser.Email);

        var user = await _repository.GetById(loggedUser.Id);

        user.Name = request.Name;
        user.Email = request.Email;

        _repository.Update(user);

        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestUpdateUser request, string currentEmail)
    {
        var validator = new UpdateUserValidator();

        var result = validator.Validate(request);

        if (currentEmail.Equals(request.Email).IsFalse())
        {
            var userExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
            if (userExist)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", MessagesException.EMAIL_ALREADY_REGISTARED));
        }

        if (result.IsValid.IsFalse())
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
