using FluentValidation;
using MyGeneralNotes.Communication.Requests;

namespace MyGeneralNotes.Application.UseCases.Routines.Update;
public class UpdateRoutineValidator : AbstractValidator<RequestRoutine>
{
    public UpdateRoutineValidator()
    {
        RuleFor(x => x).SetValidator(new RoutineValidator());
    }
}
