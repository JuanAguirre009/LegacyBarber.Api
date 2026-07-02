using LegacyBarber.App.Core.Model.Identity;
using LegacyBarber.App.Core.Services.BarberiaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegacyBarber.App.Api.Controllers
{
    [ApiController]
    [Route("api/barberias")]
    [AllowAnonymous]
    public class BarberiasPublicasController : ControllerBase
    {
        private readonly IBarberiaService barberiaService;

        public BarberiasPublicasController(IBarberiaService barberiaService)
        {
            this.barberiaService = barberiaService;
        }

        [HttpGet("disponibles")]
        public async Task<IActionResult> GetDisponibles(CancellationToken cancellationToken)
        {
            IReadOnlyList<BarberiaResumenModel> barberias = await barberiaService.GetDisponiblesAsync(cancellationToken);
            return Ok(barberias);
        }
    }
}
