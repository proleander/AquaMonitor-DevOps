using AquaMonitor.Api.ViewModels;
using FluentValidation;

namespace AquaMonitor.Api.Validators
{
    public class CreateConsumoAguaValidator : AbstractValidator<CreateConsumoAguaViewModel>
    {
        public CreateConsumoAguaValidator()
        {
            RuleFor(x => x.Local)
                .NotEmpty().WithMessage("O local é obrigatório.")
                .MaximumLength(100).WithMessage("O local deve ter no máximo 100 caracteres.");

            RuleFor(x => x.LitrosConsumidos)
                .GreaterThan(0).WithMessage("O consumo deve ser maior que zero.");

            RuleFor(x => x.NivelAlerta)
                .InclusiveBetween(0, 5).WithMessage("O nível de alerta deve estar entre 0 e 5.");
        }
    }
}
