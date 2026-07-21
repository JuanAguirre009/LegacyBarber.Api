using FluentValidation;
using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Validators.Identity
{
    public sealed class CrearBarberiaValidator : AbstractValidator<CrearBarberiaModel>
    {
        public CrearBarberiaValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la barbería es obligatorio.")
                .MaximumLength(200).WithMessage("El nombre no puede superar los 200 caracteres.");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("El slug es obligatorio.")
                .MaximumLength(150).WithMessage("El slug no puede superar los 150 caracteres.")
                .Matches(@"^[a-z0-9]+(-[a-z0-9]+)*$").WithMessage("El slug solo puede contener letras minúsculas, números y guiones, y no puede comenzar ni terminar con guion.");

            RuleFor(x => x.Direccion)
                .NotEmpty().WithMessage("La dirección es obligatoria.")
                .MaximumLength(300).WithMessage("La dirección no puede superar los 300 caracteres.");

            RuleFor(x => x.Ciudad)
                .NotEmpty().WithMessage("La ciudad es obligatoria.")
                .MaximumLength(100).WithMessage("La ciudad no puede superar los 100 caracteres.");

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .MaximumLength(50).WithMessage("El teléfono no puede superar los 50 caracteres.");

            RuleFor(x => x.EmailContacto)
                .NotEmpty().WithMessage("El email de contacto es obligatorio.")
                .EmailAddress().WithMessage("El email de contacto no tiene un formato válido.")
                .MaximumLength(200).WithMessage("El email no puede superar los 200 caracteres.");
        }
    }
}
