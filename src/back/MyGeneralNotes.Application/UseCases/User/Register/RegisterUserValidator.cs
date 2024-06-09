using FluentValidation;
using MyGeneralNotes.Application.SharedValidators;
using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Domain.Extensions;
using MyGeneralNotes.Exceptions;

namespace MyGeneralNotes.Application.UseCases.User.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisteredUser>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(MessagesException.NAME_EMPTY);
        RuleFor(user => user.Email).NotEmpty().WithMessage(MessagesException.EMAIL_EMPTY);
        RuleFor(user => user.Email).EmailAddress().WithMessage(MessagesException.EMAIL_IS_INVALID);
        RuleFor(user => user.Password).NotEmpty();
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisteredUser>());

        When(user => string.IsNullOrWhiteSpace(user.Email).IsFalse(), () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(MessagesException.EMAIL_IS_INVALID);
        });
    }
}
