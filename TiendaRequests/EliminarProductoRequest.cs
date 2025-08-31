using System.ComponentModel.DataAnnotations;

namespace TiendaRequests
{
    public class EliminarProductoRequest
    {
        [Required(ErrorMessage = "El ID del producto es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID debe ser mayor a 0")]
        public int Id { get; set; }
    }
}
