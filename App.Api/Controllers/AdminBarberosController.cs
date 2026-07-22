using LegacyBarber.App.Api.Security;
using LegacyBarber.App.Core.Interfaces.Identity;
using LegacyBarber.App.Core.Model.Identity;
using LegacyBarber.App.Core.Services.BarberoService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegacyBarber.App.Api.Controllers
{
    [ApiController]
    [Route("api/barberias/mias/barberos")]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class AdminBarberosController : ControllerBase
    {
        private readonly IBarberoService barberoService;
        private readonly ICurrentUserService currentUserService;

        public AdminBarberosController(IBarberoService barberoService, ICurrentUserService currentUserService)
        {
            this.barberoService = barberoService;
            this.currentUserService = currentUserService;
        }

        private long BarberiaId => currentUserService.BarberiaId ?? throw new InvalidOperationException("No tenés una barbería asignada.");

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            IReadOnlyList<BarberoModel> barberos = await barberoService.GetAllAsync(BarberiaId, cancellationToken);
            return Ok(barberos);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        {
            BarberoModel barbero = await barberoService.GetByIdAsync(BarberiaId, id, cancellationToken);
            return Ok(barbero);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CrearBarberoModel model, CancellationToken cancellationToken)
        {
            BarberoModel barbero = await barberoService.CreateAsync(BarberiaId, model, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = barbero.Id }, barbero);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] ActualizarBarberoModel model, CancellationToken cancellationToken)
        {
            BarberoModel barbero = await barberoService.UpdateAsync(BarberiaId, id, model, cancellationToken);
            return Ok(barbero);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            bool deleted = await barberoService.DeleteAsync(BarberiaId, id, cancellationToken);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
