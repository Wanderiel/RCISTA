using ApplicationCore.Dtos.NPMs;
using ApplicationCore.Services;
using Domain.Models.NPMs;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class NonPaperMediaController : Controller
{
    private readonly NonPaperMediaService _service;

    public NonPaperMediaController(NonPaperMediaService npmService)
    {
        _service = npmService;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreatedNPMdto dto)
    {
        await _service.Create(dto);

        return Ok();
    }

    [HttpGet]
    public async Task<List<NonPaperMedia>> GetAll() =>
        await _service.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<NonPaperMedia>> Get(int id)
    {
        NonPaperMedia? nonPaperMedia = await _service.GetAsync(id);

        if (nonPaperMedia == null)
            return NotFound();

        return Ok(nonPaperMedia);
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
