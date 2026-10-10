using ApplicationCore.Interfaces;
using Domain.Models.NPMs;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NonPaperMediaRepository : INonPaperMediaRepository
{
    private readonly SQLiteContext _context;

    public NonPaperMediaRepository(SQLiteContext context) =>
        _context = context;

    public void Insert(NonPaperMedia nonPaperMedia) =>
        _context.NonPaperMedias.Add(nonPaperMedia);

    public async Task<List<NonPaperMedia>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.NonPaperMedias
            .Include(d => d.Model)
            .ToListAsync(cancellationToken);

    public async Task<NonPaperMedia?> GetByIdAsync(NpmId id, CancellationToken cancellationToken = default) =>
        await _context.NonPaperMedias
            .Include(d => d.Model)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
}
