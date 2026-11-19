using ApplicationCore.Interfaces;
using Domain.Models.Workstations;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WorkstationRepository : IWorkstationRepository
{
    private readonly PostgresContext _context;

    public WorkstationRepository(PostgresContext context) =>
        _context = context;

    public void Insert(Workstation workstation) =>
        _context.Workstations.Add(workstation);

    public async Task<List<Workstation>> GetAll(CancellationToken cancellationToken = default) =>
        await _context.Workstations
            .Include(w => w.Disks)
                .ThenInclude(d => d.Manufacturer)
            .Include(w => w.Disks)
                .ThenInclude(d => d.Model)
            .ToListAsync(cancellationToken);

    public async Task<Workstation?> GetById(WorkstationId workstationId, CancellationToken cancellationToken = default) =>
        await _context.Workstations
            .Include(w => w.Disks)
                .ThenInclude(d => d.Manufacturer)
            .Include(w => w.Disks)
                .ThenInclude(d => d.Model)
            .FirstOrDefaultAsync(w => w.Id == workstationId, cancellationToken);
}
