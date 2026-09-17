using Application.Interfaces;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class NonPaperMediaRepository : INonPaperMediaRepository
{
    private readonly SQLiteContext _context;

    public NonPaperMediaRepository(SQLiteContext context) =>
        _context = context;
}
