using ApplicationCore.Dtos.WsHistory;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WsHistoryController : Controller
{
    private readonly WsHistoryService _service;

    public WsHistoryController(WsHistoryService service) =>
        _service = service;

    [HttpGet]
    public async Task<List<WsHistoryResponse>> GetAll(CancellationToken cancellationToken = default) =>
        await _service.GetAllAsync(cancellationToken);
}
