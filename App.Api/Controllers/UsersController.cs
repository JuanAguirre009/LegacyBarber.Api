using LegacyBarber.App.Api.Security;
using LegacyBarber.App.Core.Interfaces.Identity;
using LegacyBarber.App.Core.Model.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegacyBarber.App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = ApplicationRoles.SuperAdmin)]
    public class UsersController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        public UsersController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<UsuarioModel> users = await usuarioService.GetAllAsync(cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        {
            UsuarioModel? user = await usuarioService.GetByIdAsync(id, cancellationToken);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CrearUsuarioModel model, CancellationToken cancellationToken)
        {
            UsuarioModel user = await usuarioService.CreateAsync(model, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] ActualizarUsuarioModel model, CancellationToken cancellationToken)
        {
            UsuarioModel? user = await usuarioService.UpdateAsync(id, model, cancellationToken);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            bool deleted = await usuarioService.DeleteAsync(id, cancellationToken);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
