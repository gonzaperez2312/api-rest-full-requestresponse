using Microsoft.AspNetCore.Mvc;
using UsuarioRequests;
using UsuarioResponses;
using UsuariosService;

namespace MiApp.API.Controllers
{
    [Route("usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private UsuarioService usuarioService = new UsuarioService();

        [HttpPost]
        public IActionResult CrearUsuario([FromBody] CrearUsuarioRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = usuarioService.CrearUsuario(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet]
        public IActionResult ObtenerTodosUsuarios()
        {
            var response = usuarioService.ObtenerTodosUsuarios();

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerUsuarioPorId(int id)
        {
            var request = new ObtenerUsuarioRequest { Id = id };
            
            if (!TryValidateModel(request))
            {
                return BadRequest(ModelState);
            }

            var response = usuarioService.ObtenerUsuarioPorId(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarUsuario(int id, [FromBody] ActualizarUsuarioRequest request)
        {
            if (request == null)
            {
                request = new ActualizarUsuarioRequest();
            }
            
            request.Id = id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = usuarioService.ActualizarUsuario(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            var request = new EliminarUsuarioRequest { Id = id };
            
            if (!TryValidateModel(request))
            {
                return BadRequest(ModelState);
            }

            var response = usuarioService.EliminarUsuario(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return NotFound(response);
        }
    }
}