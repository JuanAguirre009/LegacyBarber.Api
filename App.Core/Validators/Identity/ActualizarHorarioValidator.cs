using FluentValidation;
using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Validators.Identity
{
    public sealed class ActualizarHorarioValidator : AbstractValidator<ActualizarHorarioModel>
    {
        public ActualizarHorarioValidator()
        {
            RuleFor(x => x.HorarioAtencion)
                .NotEmpty().WithMessage("El horario de atención es obligatorio.");
        }
    }
}
