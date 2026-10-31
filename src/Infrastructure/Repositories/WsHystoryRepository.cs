using ApplicationCore.Interfaces;
using Domain.Models.History;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WsHystoryRepository : IWsHystoryRepository
{
    private PostgresContext _context;

    public WsHystoryRepository(PostgresContext context) =>
        _context = context;

    public void Insert(WorkstationHistoryItem item) =>
        _context.WorkstationHistoryItems.Add(item);

    public async Task<List<WorkstationHistoryItem>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.WorkstationHistoryItems.ToListAsync(cancellationToken: cancellationToken);
}
