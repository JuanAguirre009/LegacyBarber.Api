using LegacyBarber.App.Api.Security;
using LegacyBarber.App.Core.Interfaces.Identity;
using LegacyBarber.App.Core.Model.Identity;
using LegacyBarber.App.Core.Services.CategoriaServicioService;
using LegacyBarber.App.Core.Services.ServicioService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegacyBarber.App.Api.Controllers
{
    [ApiController]
    [Route("api/barberias/mias")]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class AdminCatalogController : ControllerBase
    {
        private readonly ICategoriaServicioService categoriaService;
        private readonly IServicioService servicioService;
        private readonly ICurrentUserService currentUserService;

        public AdminCatalogController(
            ICategoriaServicioService categoriaService,
            IServicioService servicioService,
            ICurrentUserService currentUserService)
        {
            this.categoriaService = categoriaService;
            this.servicioService = servicioService;
            this.currentUserService = currentUserService;
        }

        private long BarberiaId => currentUserService.BarberiaId ?? throw new InvalidOperationException("No tenés una barbería asignada.");

        [HttpGet("categorias")]
        public async Task<IActionResult> GetCategorias(CancellationToken cancellationToken)
        {
            IReadOnlyList<CategoriaModel> categorias = await categoriaService.GetAllAsync(BarberiaId, cancellationToken);
            return Ok(categorias);
        }

        [HttpPost("categorias")]
        public async Task<IActionResult> CreateCategoria([FromBody] CrearCategoriaModel model, CancellationToken cancellationToken)
        {
            CategoriaModel categoria = await categoriaService.CreateAsync(BarberiaId, model, cancellationToken);
            return CreatedAtAction(nameof(GetCategorias), new { id = categoria.Id }, categoria);
        }

        [HttpPut("categorias/{id:long}")]
        public async Task<IActionResult> UpdateCategoria(long id, [FromBody] ActualizarCategoriaModel model, CancellationToken cancellationToken)
        {
            CategoriaModel categoria = await categoriaService.UpdateAsync(BarberiaId, id, model, cancellationToken);
            return Ok(categoria);
        }

        [HttpDelete("categorias/{id:long}")]
        public async Task<IActionResult> DeleteCategoria(long id, CancellationToken cancellationToken)
        {
            bool deleted = await categoriaService.DeleteAsync(BarberiaId, id, cancellationToken);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("servicios")]
        public async Task<IActionResult> GetServicios([FromQuery] long? categoriaId, CancellationToken cancellationToken)
        {
            IReadOnlyList<ServicioModel> servicios = await servicioService.GetAllAsync(BarberiaId, categoriaId, cancellationToken);
            return Ok(servicios);
        }

        [HttpGet("servicios/{id:long}")]
        public async Task<IActionResult> GetServicio(long id, CancellationToken cancellationToken)
        {
            ServicioModel servicio = await servicioService.GetByIdAsync(BarberiaId, id, cancellationToken);
            return Ok(servicio);
        }

        [HttpPost("servicios")]
        public async Task<IActionResult> CreateServicio([FromBody] CrearServicioModel model, CancellationToken cancellationToken)
        {
            ServicioModel servicio = await servicioService.CreateAsync(BarberiaId, model, cancellationToken);
            return CreatedAtAction(nameof(GetServicio), new { id = servicio.Id }, servicio);
        }

        [HttpPut("servicios/{id:long}")]
        public async Task<IActionResult> UpdateServicio(long id, [FromBody] ActualizarServicioModel model, CancellationToken cancellationToken)
        {
            ServicioModel servicio = await servicioService.UpdateAsync(BarberiaId, id, model, cancellationToken);
            return Ok(servicio);
        }

        [HttpPatch("servicios/{id:long}/activar")]
        public async Task<IActionResult> ToggleServicio(long id, [FromBody] ActivarServicioModel model, CancellationToken cancellationToken)
        {
            ServicioModel servicio = await servicioService.ToggleActivoAsync(BarberiaId, id, model.Activo, cancellationToken);
            return Ok(servicio);
        }

        [HttpDelete("servicios/{id:long}")]
        public async Task<IActionResult> DeleteServicio(long id, CancellationToken cancellationToken)
        {
            bool deleted = await servicioService.DeleteAsync(BarberiaId, id, cancellationToken);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
