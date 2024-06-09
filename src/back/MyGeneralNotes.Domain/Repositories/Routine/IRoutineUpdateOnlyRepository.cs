namespace MyGeneralNotes.Domain.Repositories.Routine;
public interface IRoutineUpdateOnlyRepository
{
    Task<Entities.Routine?> GetById(long routineId);
    void Update(Entities.Routine routine);
}
