using ApplicationCore.Dtos.WsHistory;
using ApplicationCore.Extentions;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services;

public class WsHistoryService
{
    private readonly IWsHistoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public WsHistoryService(IWsHistoryRepository wsHistoryRepository, IUnitOfWork unitOfWork)
    {
        _repository = wsHistoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<WsHistoryResponse>> GetAllAsync()
    {
        return (await _repository.GetAllAsync()).Select(r => r.ToDto()).ToList();
    }
}
