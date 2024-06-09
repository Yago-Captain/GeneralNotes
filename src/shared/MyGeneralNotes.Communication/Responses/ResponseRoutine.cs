using MyGeneralNotes.Communication.Requests;

namespace MyGeneralNotes.Communication.Responses;
public class ResponseRoutine
{
    public ResponseRoutine()
    {
        Exercises = [];
    }
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DayOfWeek DayOfWeek { get; set; }
    public List<RequestExercise> Exercises { get; set; }
}
