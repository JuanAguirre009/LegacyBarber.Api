using FluentValidation;
using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Validators.Identity
{
    public sealed class ActualizarBarberoValidator : AbstractValidator<ActualizarBarberoModel>
    {
        public ActualizarBarberoValidator()
        {
            RuleFor(x => x.NombreCompleto)
                .NotEmpty().WithMessage("El nombre completo es obligatorio.")
                .MaximumLength(200).WithMessage("El nombre no puede superar los 200 caracteres.");

            RuleFor(x => x.Telefono)
                .MaximumLength(50).WithMessage("El teléfono no puede superar los 50 caracteres.");

            RuleFor(x => x.Biografia)
                .MaximumLength(500).WithMessage("La biografía no puede superar los 500 caracteres.");

            RuleFor(x => x.ComisionPorcentaje)
                .InclusiveBetween(0, 100).WithMessage("La comisión debe estar entre 0 y 100.");
        }
    }
}
