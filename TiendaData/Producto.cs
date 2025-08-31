namespace TiendaData
{
    public class Producto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; } = string.Empty; // Laptop, Smartphone, Tablet, etc.
        public string Especificaciones { get; set; } = string.Empty; // JSON string con especificaciones técnicas
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
    }
}
