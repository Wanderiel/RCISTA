using ApplicationCore.Services;
using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly UserService _service;

    public UserController(UserService service) =>
        _service = service;

    [HttpGet]
    public async Task<List<User>> GetAll() =>
        await _service.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id)
    {
        User? user = await _service.GetAsync(id);

        if (user == null)
            return NotFound();

        return Ok(user);
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
