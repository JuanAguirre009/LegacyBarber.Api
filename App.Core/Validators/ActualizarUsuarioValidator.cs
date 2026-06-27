using FluentValidation;
using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Validators
{
    public sealed class ActualizarUsuarioValidator : AbstractValidator<ActualizarUsuarioModel>
    {
        public ActualizarUsuarioValidator()
        {
            RuleFor(x => x.NombreCompleto)
                .NotEmpty().WithMessage("El nombre completo es obligatorio.")
                .MaximumLength(200).WithMessage("El nombre completo no puede exceder 200 caracteres.");

            RuleFor(x => x.Roles)
                .NotNull().WithMessage("Los roles son obligatorios.")
                .Must(roles => roles.Count > 0).WithMessage("Debe asignar al menos un rol.");

            RuleForEach(x => x.Roles)
                .NotEmpty().WithMessage("El rol no puede estar vacío.");
        }
    }
}
