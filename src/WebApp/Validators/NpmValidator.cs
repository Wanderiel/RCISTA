using ApplicationCore.Dtos.NPMs;
using ApplicationCore.Interfaces;
using FluentValidation;

namespace WebApp.Validators;

public class NpmValidator : AbstractValidator<NpmRequest>
{
    private readonly INpmLookup _lookup;

    public NpmValidator(INpmLookup npmLookup)
    {
        _lookup = npmLookup;

        RuleFor(d => d.Type)
            .NotNull().WithMessage("Тип носителя обязателен.")
            .IsInEnum().WithMessage("Указан неверный тип носителя.");

        RuleFor(d => d.Manufacturer)
            .NotEmpty().WithMessage("Название Производителя не может быть пустое.");

        RuleFor(d => d.Model)
            .NotEmpty().WithMessage("Название Модели не может быть пустое.");

        RuleFor(d => d.SerialNumber)
            .NotEmpty().WithMessage("Серийный номер не может быть пустым.")
            .MustAsync(IsSerialnumberAvailable).WithMessage("Носитель с таким серийным номером существует.");

        RuleFor(d => d.Capacity)
            .GreaterThan(0).WithMessage("Ёмкость должна быть бльше нуля.");
    }

    private async Task<bool> IsSerialnumberAvailable(string serialnumber, CancellationToken cancellationToken = default) =>
        await _lookup.HasNpmBySerialnumberAsync(serialnumber, cancellationToken);
}
