using MyGeneralNotes.Communication.Enum;

namespace MyGeneralNotes.Communication.Requests;

public class RequestExercise
{
    public string Name { get; set; } = string.Empty;
    public ExerciseLocation Location { get; set; }
    public double Charge { get; set; }
    public int Repetitions { get; set; }
    public int RestTime { get; set; }
    public string Equipment { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}