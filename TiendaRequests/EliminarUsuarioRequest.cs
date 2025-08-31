using System.ComponentModel.DataAnnotations;

namespace TiendaRequests
{
    public class EliminarUsuarioRequest
    {
        [Required(ErrorMessage = "El ID del usuario es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario debe ser mayor a 0")]
        public int Id { get; set; }
    }
}
