using Microsoft.AspNetCore.Mvc;
using TiendaRequests;
using TiendaResponses;
using TiendaService;

namespace MiApp.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar productos electrónicos
    /// </summary>
    [Route("productos")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private ProductoService productoService = new ProductoService();

        /// <summary>
        /// Crea un nuevo producto
        /// </summary>
        /// <param name="request">Datos del producto a crear</param>
        /// <returns>Producto creado exitosamente</returns>
        [HttpPost]
        public IActionResult CrearProducto([FromBody] CrearProductoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = productoService.CrearProducto(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        /// <summary>
        /// Obtiene todos los productos
        /// </summary>
        /// <returns>Lista de todos los productos</returns>
        [HttpGet]
        public IActionResult ObtenerTodosProductos()
        {
            var response = productoService.ObtenerTodosProductos();

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        /// <summary>
        /// Obtiene un producto por su ID
        /// </summary>
        /// <param name="id">ID del producto</param>
        /// <returns>Producto encontrado</returns>
        [HttpGet("{id}")]
        public IActionResult ObtenerProductoPorId(int id)
        {
            var request = new ObtenerProductoRequest { Id = id };
            
            if (!TryValidateModel(request))
            {
                return BadRequest(ModelState);
            }

            var response = productoService.ObtenerProductoPorId(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        /// <summary>
        /// Actualiza un producto existente
        /// </summary>
        /// <param name="id">ID del producto a actualizar</param>
        /// <param name="request">Datos actualizados del producto</param>
        /// <returns>Producto actualizado exitosamente</returns>
        [HttpPut("{id}")]
        public IActionResult ActualizarProducto(int id, [FromBody] ActualizarProductoRequest request)
        {
            if (request == null)
            {
                request = new ActualizarProductoRequest();
            }
            
            request.Id = id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = productoService.ActualizarProducto(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        /// <summary>
        /// Elimina un producto (marca como eliminado)
        /// </summary>
        /// <param name="id">ID del producto a eliminar</param>
        /// <returns>Producto eliminado exitosamente</returns>
        [HttpDelete("{id}")]
        public IActionResult EliminarProducto(int id)
        {
            var request = new EliminarProductoRequest { Id = id };
            
            if (!TryValidateModel(request))
            {
                return BadRequest(ModelState);
            }

            var response = productoService.EliminarProducto(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return NotFound(response);
        }
    }
}
