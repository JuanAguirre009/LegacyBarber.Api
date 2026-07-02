using LegacyBarber.App.Core.Interfaces.Identity;
using LegacyBarber.App.Core.Model.Identity;
using LegacyBarber.App.Core.Services.ClienteService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegacyBarber.App.Api.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService clienteService;
        private readonly ICurrentUserService currentUserService;

        public ClientesController(IClienteService clienteService, ICurrentUserService currentUserService)
        {
            this.clienteService = clienteService;
            this.currentUserService = currentUserService;
        }

        [HttpPost("asociar-barberia")]
        public async Task<IActionResult> AsociarBarberia([FromBody] AsociarBarberiaModel model, CancellationToken cancellationToken)
        {
            if (!currentUserService.UsuarioId.HasValue)
                return Unauthorized();

            LoginResponseModel response = await clienteService.AsociarBarberiaAsync(
                currentUserService.UsuarioId.Value,
                model.BarberiaId,
                cancellationToken);

            return Ok(response);
        }
    }
}
