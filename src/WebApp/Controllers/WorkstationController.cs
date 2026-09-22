using ApplicationCore.Services;
using Domain.Models.Workstations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkstationController : Controller
{
    private readonly WorkstationService _service;

    public WorkstationController(WorkstationService service) =>
        _service = service;

    [HttpGet]
    public async Task<List<Workstation>> GetAll() =>
        await _service.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Workstation>> Get(int id)
    {
        Workstation? workstation = await _service.GetAsync(id);

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
}
