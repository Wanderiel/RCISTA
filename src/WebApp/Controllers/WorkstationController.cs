using ApplicationCore.Dtos.WorkStations;
using ApplicationCore.Dtos.WsHistory;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkstationController : Controller
{
    private readonly WorkstationService _service;

    public WorkstationController(WorkstationService service) =>
        _service = service;

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreatedWorkstationDto dto)
    {
        await _service.Create(dto);

        return Created();
    }

    [HttpGet]
    public async Task<List<WorkstationResponse>> GetAll() =>
        await _service.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkstationResponse>> Get(int id)
    {
        WorkstationResponse? workstation = await _service.GetAsync(id);

        if (workstation == null)
            return NotFound();

        return Ok(workstation);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        bool result = await _service.DeleteAsync(id);

        if (result == false)
            return NotFound();

        return Ok();
    }

    [HttpPut("CreateTask")]
    public async Task<IActionResult> CreateTask([FromBody] TaskRequest taskDto, CancellationToken cancellationToken = default)
    {
        await _service.CreateTask(taskDto, cancellationToken);

        return Ok();
    }
}
