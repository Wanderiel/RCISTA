using ApplicationCore.Dtos.NPMs;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class NonPaperMediaController : Controller
{
    private readonly NonPaperMediaService _service;

    public NonPaperMediaController(NonPaperMediaService npmService) =>
        _service = npmService;

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] NpmRequest dto, CancellationToken cancellationToken = default)
    {
        await _service.Create(dto, cancellationToken);

        return Created();
    }

    [HttpGet]
    public async Task<List<NpmResponse>> GetAll(CancellationToken cancellationToken = default) =>
        await _service.GetAllAsync(cancellationToken);

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id, CancellationToken cancellationToken = default)
    {
        NpmResponse? nonPaperMedia = await _service.GetAsync(id, cancellationToken);

        return Ok(nonPaperMedia);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        bool result = await _service.DeleteAsync(id, cancellationToken);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatedNPMDto npmDto, CancellationToken cancellationToken = default)
    {
        bool result = await _service.UpdateAsync(id, npmDto, cancellationToken);

        if (result == false)
            return BadRequest();

        return Ok();
    }
}
