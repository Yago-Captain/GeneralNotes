using FluentValidation;
using MyGeneralNotes.Communication.Requests;
using MyGeneralNotes.Exceptions;

namespace MyGeneralNotes.Application.UseCases.Routines;
public class RoutineValidator : AbstractValidator<RequestRoutine>
{
    public RoutineValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage(MessagesException.ROUTINE_NAME_IN_BLANK);
        RuleFor(r => r.DayOfWeek).IsInEnum().NotEmpty();
        RuleFor(r => r.Exercises).NotEmpty().WithMessage(MessagesException.EXERCISES_IN_BLANK);
        RuleForEach(r => r.Exercises).ChildRules(exercices =>
        {
            exercices.RuleFor(e => e.Name).NotEmpty().WithMessage(MessagesException.EXERCISE_NAME_BLANK);
            exercices.RuleFor(e => e.Location).IsInEnum();
            exercices.RuleFor(e => e.Charge).NotEmpty().WithMessage(MessagesException.EXERCISE_WEIGHT_IN_WHITE);
            exercices.RuleFor(e => e.Repetitions).NotEmpty().WithMessage(MessagesException.EXERCISE_REPETITIONS_BLANK);
            exercices.RuleFor(e => e.RestTime).NotEmpty().WithMessage(MessagesException.WAIT_TIME_IN_BLANK);
            exercices.RuleFor(e => e.Equipment).NotEmpty().WithMessage(MessagesException.EQUIPMENT_EXERCISE_BLANK);
            exercices.RuleFor(e => e.Details).NotEmpty().WithMessage(MessagesException.DETAIL_EXERCISE_BLANK);
        });
    }
}
