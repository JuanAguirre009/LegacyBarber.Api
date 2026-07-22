using FluentValidation;
using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Validators.Identity
{
    public sealed class ActualizarCategoriaValidator : AbstractValidator<ActualizarCategoriaModel>
    {
        public ActualizarCategoriaValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la categoría es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(300).WithMessage("La descripción no puede superar los 300 caracteres.");

            RuleFor(x => x.Orden)
                .GreaterThanOrEqualTo((short)0).WithMessage("El orden no puede ser negativo.");
        }
    }
}
