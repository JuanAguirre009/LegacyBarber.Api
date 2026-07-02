using FluentValidation;
using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Validators.Identity
{
    public sealed class AsociarBarberiaValidator : AbstractValidator<AsociarBarberiaModel>
    {
        public AsociarBarberiaValidator()
        {
            RuleFor(x => x.BarberiaId)
                .GreaterThan(0).WithMessage("Debe seleccionar una barbería válida.");
        }
    }
}
