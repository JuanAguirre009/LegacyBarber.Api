using FluentValidation;
using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Validators.Identity
{
    public sealed class CrearBarberoValidator : AbstractValidator<CrearBarberoModel>
    {
        public CrearBarberoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("El email no tiene un formato válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .Matches(@"[A-Z]").WithMessage("La contraseña debe contener al menos una mayúscula.")
                .Matches(@"[a-z]").WithMessage("La contraseña debe contener al menos una minúscula.")
                .Matches(@"[0-9]").WithMessage("La contraseña debe contener al menos un número.");

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
