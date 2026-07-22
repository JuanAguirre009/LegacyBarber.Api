using FluentValidation;
using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Validators.Identity
{
    public sealed class CrearServicioValidator : AbstractValidator<CrearServicioModel>
    {
        public CrearServicioValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del servicio es obligatorio.")
                .MaximumLength(200).WithMessage("El nombre no puede superar los 200 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres.");

            RuleFor(x => x.DuracionMinutos)
                .GreaterThan((short)0).WithMessage("La duración debe ser mayor a 0 minutos.");

            RuleFor(x => x.Precio)
                .GreaterThanOrEqualTo(0).WithMessage("El precio no puede ser negativo.");

            RuleFor(x => x.Color)
                .MaximumLength(20).WithMessage("El color no puede superar los 20 caracteres.");
        }
    }
}
