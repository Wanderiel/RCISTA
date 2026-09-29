using ApplicationCore.Dtos.Users;
using ApplicationCore.Interfaces;
using FluentValidation;

namespace WebApp.Validators;

public class RegisterValidator : AbstractValidator<RegisteredUserDto>
{
    private const int MinimumLengthUsername = 3;
    private const int MinimumLengthPassword = 8;

    private readonly IUserLookup _userLookup;

    public RegisterValidator(IUserLookup userLookup)
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Имя пользователя не может быть пустым.")
            .MinimumLength(MinimumLengthUsername).WithMessage($"Имя пользователя должно содержать минимум {MinimumLengthUsername} символа.")
            .MustAsync(IsLoginAvailable).WithMessage("Имя пользователя уже занято, придумайте другое.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Имя не может быть пустым.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Фамилия не может быть пустой.");

        RuleFor(x => x.Password1)
            .NotEmpty().WithMessage("Пароль не может быть пустым.")
            .MinimumLength(MinimumLengthPassword).WithMessage($"Пароль должен содержать минимум {MinimumLengthPassword} символов.");

        RuleFor(x => x.Password2)
            .Equal(x => x.Password1).WithMessage("Пароли должны совпадать.");
        _userLookup = userLookup;
    }

    private async Task<bool> IsLoginAvailable(string username, CancellationToken token) =>
        await _userLookup.HasUserByLoginAsync(username) == false;
}
