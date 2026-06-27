using LegacyBarber.App.Api.Security;
using LegacyBarber.App.Core.Interfaces.Identity;
using LegacyBarber.App.Core.Model.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegacyBarber.App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel request, CancellationToken cancellationToken)
        {
            TokenPairModel tokenPair = await authService.LoginAsync(request, cancellationToken);
            return Ok(tokenPair);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestModel request, CancellationToken cancellationToken)
        {
            TokenPairModel tokenPair = await authService.RefreshTokenAsync(request.TokenRefresco, cancellationToken);
            return Ok(tokenPair);
        }
    }
}
