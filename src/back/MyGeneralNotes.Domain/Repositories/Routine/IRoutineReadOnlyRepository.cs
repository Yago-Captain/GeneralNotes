namespace MyGeneralNotes.Domain.Repositories.Routine;
public interface IRoutineReadOnlyRepository
{
    Task<IList<Entities.Routine>> GetAllUserRoutines(long userId);
    Task<Entities.Routine?> GetRoutine(long routineId);
}
