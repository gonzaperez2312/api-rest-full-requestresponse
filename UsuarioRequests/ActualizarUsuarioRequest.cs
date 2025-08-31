using System.ComponentModel.DataAnnotations;

namespace UsuarioRequests
{
    public class ActualizarUsuarioRequest
    {
        [Required(ErrorMessage = "El ID del usuario es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario debe ser mayor a 0")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre completo no puede exceder los 100 caracteres")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        [StringLength(150, ErrorMessage = "El correo electrónico no puede exceder los 150 caracteres")]
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}
