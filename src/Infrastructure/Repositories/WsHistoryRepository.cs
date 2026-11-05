using ApplicationCore.Interfaces;
using Domain.Models.History;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WsHistoryRepository : IWsHistoryRepository
{
    private PostgresContext _context;

    public WsHistoryRepository(PostgresContext context) =>
        _context = context;

    public void Insert(WorkstationHistoryItem item) =>
        _context.WorkstationHistoryItems.Add(item);

    public async Task<List<WorkstationHistoryItem>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.WorkstationHistoryItems.ToListAsync(cancellationToken: cancellationToken);
}
