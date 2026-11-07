using ApplicationCore.Dtos.NPMs;
using FluentValidation;

namespace WebApp.Validators;

public class NpmValidator : AbstractValidator<NpmRequest>
{
    public NpmValidator()
    {
        RuleFor(d => d.Type)
            .IsInEnum()

        RuleFor(d => d.Manufacturer)
            .NotEmpty().WithMessage("Название Производителя не может быть пустое.");

        RuleFor(d => d.Model)
            .NotEmpty().WithMessage("Название Модели не может быть пустое.");

        RuleFor(d => d.SerialNumber)
            .NotEmpty().WithMessage("Серийный номер не может быть пустым.");

        RuleFor(d => d.Capacity)
            .GreaterThan(0).WithMessage("Ёмкость должна быть юльше нуля.");
    }
}
