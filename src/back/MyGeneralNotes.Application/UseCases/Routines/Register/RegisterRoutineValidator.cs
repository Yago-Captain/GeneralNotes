using FluentValidation;
using MyGeneralNotes.Communication.Requests;

namespace MyGeneralNotes.Application.UseCases.Routines.Register;
public class RegisterRoutineValidator : AbstractValidator<RequestRoutine>
{
    public RegisterRoutineValidator()
    {
        RuleFor(r => r).SetValidator(new RoutineValidator());
    }
}

