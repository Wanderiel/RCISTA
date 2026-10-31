using Domain.Models.History;

namespace ApplicationCore.Interfaces;

public interface IWsHystoryRepository
{
    void Insert(WorkstationHistoryItem item);
    Task<List<WorkstationHistoryItem>> GetAllAsync(CancellationToken cancellationToken = default);
}
