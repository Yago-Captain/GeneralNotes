using MyGeneralNotes.Domain.Repositories;

namespace MyGeneralNotes.Infrastructure.DataAccess;
public class UnitOfWork(MyGeneralNotesDbContext context) : IUnitOfWork
{
    private readonly MyGeneralNotesDbContext _context = context;

    public async Task Commit() => await _context.SaveChangesAsync();
}
