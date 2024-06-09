using FluentValidation;
using MyGeneralNotes.Application.SharedValidators;
using MyGeneralNotes.Communication.Requests;

namespace MyGeneralNotes.Application.UseCases.User.ChangePassword;
public class ChangePasswordValidator : AbstractValidator<RequestChangePassword>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePassword>());
    }
}
