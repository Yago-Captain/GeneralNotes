using Microsoft.EntityFrameworkCore;
using MyGeneralNotes.Domain.Entities;
using MyGeneralNotes.Domain.Repositories.Routine;

namespace MyGeneralNotes.Infrastructure.DataAccess.Repositories;
public class RoutineRepository(MyGeneralNotesDbContext context) : IRoutineWriteOnlyRepository, IRoutineReadOnlyRepository, IRoutineUpdateOnlyRepository
{
    private readonly MyGeneralNotesDbContext _context = context;

    public async Task Delete(long routineId)
    {
        var routine = await _context.Routines.FirstOrDefaultAsync(r => r.Id == routineId);
        if (routine != null)
            _context.Routines.Remove(routine);
    }

    public async Task<IList<Routine>> GetAllUserRoutines(long userId)
    {
        return await _context.Routines
            .AsNoTracking()
            .Include(ex => ex.Exercises)
            .Where(r => r.UserId == userId).ToListAsync();
    }

    public async Task<Routine?> GetById(long routineId)
    {
        return await _context.Routines
             .Include(ex => ex.Exercises)
             .FirstOrDefaultAsync(r => r.Id == routineId);
    }

    public async Task<Routine?> GetRoutine(long routineId)
    {
        return await _context.Routines
            .AsNoTracking()
            .Include(ex => ex.Exercises)
            .FirstOrDefaultAsync(r => r.Id == routineId);
    }

    public async Task Register(Routine routine) => await _context.Routines.AddAsync(routine);

    public void Update(Routine routine) => _context.Routines.Update(routine);

}
