using ApplicationCore.Dtos.Users;
using ApplicationCore.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly AuthService _service;

    public AuthController(AuthService authService) =>
        _service = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisteredUserDto userDto,
        [FromServices] IValidator<RegisteredUserDto> validator,
        CancellationToken cancellationToken = default)
    {
        ValidationResult validationResult = await validator.ValidateAsync(userDto, cancellationToken);

        if (validationResult.IsValid == false)
            return UnprocessableEntity(validationResult.Errors);

        await _service.Register(userDto, cancellationToken);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUser, CancellationToken cancellationToken = default)
    {
        if (await _service.Login(loginUser, cancellationToken) == false)
            return BadRequest("Неверное имя пользователя или пароль.");

        return Ok("Добро пожаловать!");
    }
}
