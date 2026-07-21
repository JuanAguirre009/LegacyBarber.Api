using LegacyBarber.App.Api.Security;
using LegacyBarber.App.Core.Interfaces.Identity;
using LegacyBarber.App.Core.Model.Identity;
using LegacyBarber.App.Core.Services.BarberiaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegacyBarber.App.Api.Controllers
{
    [ApiController]
    [Route("api/barberias/mias")]
    [Authorize]
    public class BarberiasController : ControllerBase
    {
        private readonly IBarberiaService barberiaService;
        private readonly ICurrentUserService currentUserService;

        public BarberiasController(IBarberiaService barberiaService, ICurrentUserService currentUserService)
        {
            this.barberiaService = barberiaService;
            this.currentUserService = currentUserService;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearBarberiaModel model, CancellationToken cancellationToken)
        {
            if (!currentUserService.UsuarioId.HasValue)
                return Unauthorized();

            LoginResponseModel response = await barberiaService.CrearAsync(
                currentUserService.UsuarioId.Value,
                model,
                cancellationToken);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetMia(CancellationToken cancellationToken)
        {
            long? barberiaId = currentUserService.BarberiaId;
            if (!barberiaId.HasValue)
                return BadRequest(new { mensaje = "No tenés una barbería asignada. Creá una o pedile al admin que te asigne." });

            BarberiaModel barberia = await barberiaService.GetMiaAsync(barberiaId.Value, cancellationToken);
            return Ok(barberia);
        }

        [HttpPut("horario")]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> ActualizarHorario([FromBody] ActualizarHorarioModel model, CancellationToken cancellationToken)
        {
            long? barberiaId = currentUserService.BarberiaId;
            if (!barberiaId.HasValue)
                return BadRequest(new { mensaje = "No tenés una barbería asignada." });

            BarberiaModel barberia = await barberiaService.ActualizarHorarioAsync(barberiaId.Value, model, cancellationToken);
            return Ok(barberia);
        }
    }
}
