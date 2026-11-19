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
    public async Task<List<User>> GetAll(CancellationToken cancellationToken = default) =>
        await _service.GetAll(cancellationToken);

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id, CancellationToken cancellationToken = default)
    {
        User user = await _service.Get(id, cancellationToken);

        return Ok(user);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        await _service.Delete(id, cancellationToken);

        return Ok();
    }
}
