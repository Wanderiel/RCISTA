using ApplicationCore.Dtos.WsHistory;
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

    public async Task<WsHistoryResponse> GetAllAsync()
    {
        return null;
    }
}
