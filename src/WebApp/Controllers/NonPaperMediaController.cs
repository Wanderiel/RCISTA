using ApplicationCore.Services;
using Domain.Models.NPMs;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class NonPaperMediaController : Controller
{
    private readonly NonPaperMediaService _npmService;

    public NonPaperMediaController(NonPaperMediaService npmService)
    {
        _npmService = npmService;
    }

    [HttpGet]
    public async Task<List<NonPaperMedia>> GetAll() =>
        await _npmService.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<NonPaperMedia>> Get(int id)
    {
        NonPaperMedia? nonPaperMedia = await _npmService.GetAsync(id);

        if (nonPaperMedia == null)
            return NotFound();

        return Ok(nonPaperMedia);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        bool result = await _npmService.DeleteAsync(id);

        if (result == false)
            return NotFound();

        return Ok();
    }
}
