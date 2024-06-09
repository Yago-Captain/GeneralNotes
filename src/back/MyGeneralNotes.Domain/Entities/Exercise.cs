using MyGeneralNotes.Domain.Enum;

namespace MyGeneralNotes.Domain.Entities;

public class Exercise : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public ExerciseLocation Location { get; set; }
    public double Charge { get; set; } // in kilograms
    public int Repetitions { get; set; } // total repetitions
    public int RestTime { get; set; } // rest time between sets
    public string Equipment { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public long RoutineId { get; set; }
}
