using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Domain.Extensions;
using MyGeneralNotes.Domain.Repositories;
using MyGeneralNotes.Domain.Repositories.User;
using MyGeneralNotes.Domain.Security.Cryptography;
using MyGeneralNotes.Domain.Services.LoggedUser;
using MyGeneralNotes.Exceptions;
using MyGeneralNotes.Exceptions.ExceptionsBase;

namespace MyGeneralNotes.Application.UseCases.User.ChangePassword;
public class ChangePasswordUseCase(ILoggedUser logged, IUserUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IPasswordEncripter passwordEncripter) : IChangePasswordUseCase
{
    private readonly ILoggedUser _logged = logged;
    private readonly IUserUpdateOnlyRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;

    public async Task Execute(RequestChangePassword request)
    {
        var loggedUser = await _logged.User();

        Validate(request, loggedUser);

        var user = await _repository.GetById(loggedUser.Id);

        user.Password = _passwordEncripter.Encrypt(request.NewPassword);

        _repository.Update(user);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePassword request, Domain.Entities.User loggedUser)
    {
        var result = new ChangePasswordValidator().Validate(request);

        var currentPasswordEncripted = _passwordEncripter.Encrypt(request.Password);

        if (currentPasswordEncripted.Equals(loggedUser.Password).IsFalse())
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, MessagesException.EMAIL_OR_PASSWORD_INVALID));
        if (result.IsValid.IsFalse())
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
    }
}
