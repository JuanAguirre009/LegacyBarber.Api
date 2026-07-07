using FluentValidation;
using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Validators.Identity
{
    public sealed class ActualizarUsuarioValidator : AbstractValidator<ActualizarUsuarioModel>
    {
        public ActualizarUsuarioValidator()
        {
            RuleFor(x => x.NombreCompleto)
                .NotEmpty().WithMessage("El nombre completo es obligatorio.")
                .MaximumLength(200).WithMessage("El nombre completo no puede superar los 200 caracteres.");

            RuleFor(x => x.Telefono)
                .MaximumLength(50).WithMessage("El teléfono no puede superar los 50 caracteres.");

            RuleFor(x => x.Roles)
                .NotNull().WithMessage("Los roles son obligatorios.")
                .Must(roles => roles.Count > 0).WithMessage("Debe asignar al menos un rol.");

            RuleForEach(x => x.Roles)
                .NotEmpty().WithMessage("Los roles no pueden contener valores vacíos.");
        }
    }
}
