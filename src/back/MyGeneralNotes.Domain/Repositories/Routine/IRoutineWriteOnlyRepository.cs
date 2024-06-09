namespace MyGeneralNotes.Domain.Repositories.Routine;
public interface IRoutineWriteOnlyRepository
{
    Task Register(Entities.Routine routine);
    Task Delete(long routineId);
}
