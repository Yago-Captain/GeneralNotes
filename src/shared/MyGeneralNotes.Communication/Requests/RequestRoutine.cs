namespace MyGeneralNotes.Communication.Requests;
public class RequestRoutine
{
    public RequestRoutine()
    {
        Exercises = [];
    }
    public string Name { get; set; } = string.Empty;
    public DayOfWeek DayOfWeek { get; set; }
    public List<RequestExercise> Exercises { get; set; }
}
