using ApplicationCore.Interfaces;
using Domain.Models.NPMs;
using Domain.Models.Workstations;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NonPaperMediaRepository : INonPaperMediaRepository
{
    private readonly PostgresContext _context;

    public NonPaperMediaRepository(PostgresContext context) =>
        _context = context;

    public void Insert(NonPaperMedia nonPaperMedia) =>
        _context.NonPaperMedias.Add(nonPaperMedia);

    public async Task<List<NonPaperMedia>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.NonPaperMedias
            .Include(npm => npm.Manufacturer)
            .Include(npm => npm.Model)
            .ToListAsync(cancellationToken);

    public async Task<NonPaperMedia?> GetByIdAsync(NpmId id, CancellationToken cancellationToken = default) =>
        await _context.NonPaperMedias
            .Include(npm => npm.Manufacturer)
            .Include(d => d.Model)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async Task<List<NonPaperMedia>> GetByIdsAsync(IEnumerable<NpmId> ids, CancellationToken cancellationToken = default)
    {
        if (ids.Any() == false)
            return new List<NonPaperMedia>();

        List<NonPaperMedia> result = await _context.NonPaperMedias
            .Where(d => ids.Contains(d.Id))
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<bool> IsAvailableAsync(NpmId id, CancellationToken cancellationToken = default) =>
        await _context.NonPaperMedias.AnyAsync(
            disk => disk.Id == id && EF.Property<WorkstationId?>(disk, "WorkstationId") == null, cancellationToken);

    public Task<bool> HasNpmBySerialnumberAsync(string serialnumber, CancellationToken cancellationToken) =>
        _context.NonPaperMedias.AnyAsync(d => d.SerialNumber == serialnumber, cancellationToken);
}
