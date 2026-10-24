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

    public async Task<List<Workstation>> GetAllAsync() =>
        await _context.Workstations
            .Include(w => w.Disks)
                .ThenInclude(d => d.Manufacturer)
            .Include(w => w.Disks)
                .ThenInclude(d => d.Model)
            .ToListAsync();

    public async Task<Workstation?> GetByIdAsync(WorkstationId workstationId) =>
        await _context.Workstations
            .Include(w => w.Disks)
                .ThenInclude(d => d.Manufacturer)
            .Include(w => w.Disks)
                .ThenInclude(d => d.Model)
            .FirstOrDefaultAsync(w => w.Id == workstationId);
}
