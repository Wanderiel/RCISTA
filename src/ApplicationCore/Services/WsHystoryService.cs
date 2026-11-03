using ApplicationCore.Dtos.WsHistory;
using ApplicationCore.Extentions;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services;

public class WsHystoryService
{
    private readonly IWsHystoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public WsHystoryService(IWsHystoryRepository wsHystoryRepository, IUnitOfWork unitOfWork)
    {
        _repository = wsHystoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<WsHistoryResponse>> GetAllAsync()
    {
        return (await _repository.GetAllAsync()).Select(r => r.ToDto()).ToList();
    }
}
