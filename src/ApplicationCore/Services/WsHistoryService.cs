using ApplicationCore.Dtos.WsHistory;
using ApplicationCore.Extentions;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services;

public class WsHistoryService
{
    private readonly IWsHistoryRepository _repository;
    public WsHistoryService(IWsHistoryRepository wsHistoryRepository) =>
        _repository = wsHistoryRepository;

    public async Task<List<WsHistoryResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return (await _repository.GetAllAsync(cancellationToken)).Select(r => r.ToDto()).ToList();
    }
}
