using Domain.Models.History;

namespace ApplicationCore.Interfaces;

public interface IWsHistoryRepository
{
    void Insert(WorkstationHistoryItem item);
    Task<List<WorkstationHistoryItem>> GetAll(CancellationToken cancellationToken = default);
}
