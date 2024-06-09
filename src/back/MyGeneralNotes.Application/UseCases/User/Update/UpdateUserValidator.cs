using FluentValidation;
using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Domain.Extensions;
using MyGeneralNotes.Exceptions;

namespace MyGeneralNotes.Application.UseCases.User.Update;
public class UpdateUserValidator : AbstractValidator<RequestUpdateUser>
{
    public UpdateUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage(MessagesException.NAME_EMPTY);
        RuleFor(request => request.Email).NotEmpty().WithMessage(MessagesException.EMAIL_EMPTY);

        When(request => string.IsNullOrWhiteSpace(request.Email).IsFalse(), () =>
        {
            RuleFor(request => request.Email).EmailAddress().WithMessage(MessagesException.EMAIL_IS_INVALID);
        });
    }
}
