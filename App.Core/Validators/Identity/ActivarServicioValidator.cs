using FluentValidation;
using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Validators.Identity
{
    public sealed class ActivarServicioValidator : AbstractValidator<ActivarServicioModel>
    {
        public ActivarServicioValidator()
        {
            RuleFor(x => x.Activo)
                .NotNull().WithMessage("El campo activo es obligatorio.");
        }
    }
}
