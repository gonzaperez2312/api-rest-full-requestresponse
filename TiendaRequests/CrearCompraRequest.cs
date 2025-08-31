using System.ComponentModel.DataAnnotations;

namespace TiendaRequests
{
    public class CrearCompraRequest
    {
        [Required(ErrorMessage = "El ID del usuario es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario debe ser mayor a 0")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El código del producto es obligatorio")]
        [StringLength(50, ErrorMessage = "El código del producto no puede exceder los 50 caracteres")]
        public string CodigoProducto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio sin impuestos es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio sin impuestos debe ser mayor a 0")]
        public decimal PrecioSinImpuestos { get; set; }

        [Required(ErrorMessage = "El precio final es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio final debe ser mayor a 0")]
        public decimal PrecioFinal { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder los 500 caracteres")]
        public string? Observaciones { get; set; }
    }
}
