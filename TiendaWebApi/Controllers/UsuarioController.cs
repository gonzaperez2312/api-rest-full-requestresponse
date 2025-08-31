using Microsoft.AspNetCore.Mvc;
using TiendaRequests;
using TiendaResponses;
using TiendaService;

namespace MiApp.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar usuarios y sus compras
    /// </summary>
    [Route("usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private UsuarioService usuarioService = new UsuarioService();

        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        /// <param name="request">Datos del usuario a crear</param>
        /// <returns>Usuario creado exitosamente</returns>
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

        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <returns>Lista de todos los usuarios</returns>
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

        /// <summary>
        /// Obtiene un usuario por su ID
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <returns>Usuario encontrado</returns>
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

        /// <summary>
        /// Actualiza un usuario existente
        /// </summary>
        /// <param name="id">ID del usuario a actualizar</param>
        /// <param name="request">Datos actualizados del usuario</param>
        /// <returns>Usuario actualizado exitosamente</returns>
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

        /// <summary>
        /// Elimina un usuario (marca como eliminado)
        /// </summary>
        /// <param name="id">ID del usuario a eliminar</param>
        /// <returns>Usuario eliminado exitosamente</returns>
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

        /// <summary>
        /// Crea una nueva compra para un usuario específico
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <param name="request">Datos de la compra a crear</param>
        /// <returns>Compra creada exitosamente</returns>
        [HttpPost("{id}/compra")]
        public IActionResult CrearCompra(int id, [FromBody] CrearCompraRequest request)
        {
            if (request == null)
            {
                request = new CrearCompraRequest();
            }
            
            request.UsuarioId = id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = usuarioService.CrearCompra(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}