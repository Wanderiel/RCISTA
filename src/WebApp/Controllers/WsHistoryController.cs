using ApplicationCore.Dtos.WsHistory;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WsHistoryController : Controller
{
    private readonly WsHystoryService _service;

    public WsHistoryController(WsHystoryService service) =>
        _service = service;

    [HttpGet]
    public async Task<List<WsHistoryResponse>> GetAll() =>
        await _service.GetAllAsync();
}
