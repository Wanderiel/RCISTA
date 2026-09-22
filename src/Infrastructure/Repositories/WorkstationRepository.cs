using ApplicationCore.Interfaces;
using Domain.Models.Workstations;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WorkstationRepository : IWorkstationRepository
{
    private readonly SQLiteContext _context;

    public WorkstationRepository(SQLiteContext context) =>
        _context = context;

    public async Task<List<Workstation>> GetAllAsync() =>
        await _context.Workstations.ToListAsync();

    public async Task<Workstation?> GetByIdAsync(WorkstationId workstationId) =>
        await _context.Workstations.FindAsync(workstationId);

    public void Delete(Workstation workstation) =>
        _context.Remove(workstation);
}
