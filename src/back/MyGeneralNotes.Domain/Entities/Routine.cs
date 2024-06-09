namespace MyGeneralNotes.Domain.Entities;
public class Routine : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public DayOfWeek DayOfWeek { get; set; }
    public List<Exercise> Exercises { get; set; } = [];
    public long UserId { get; set; }
}
