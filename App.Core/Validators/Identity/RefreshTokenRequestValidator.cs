using FluentValidation;
using LegacyBarber.App.Core.Model.Identity;

namespace LegacyBarber.App.Core.Validators.Identity
{
    public sealed class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequestModel>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.TokenRefresco)
                .NotEmpty().WithMessage("El refresh token es obligatorio.");
        }
    }
}
