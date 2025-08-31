namespace TiendaData
{
    public class Compra
    {
        public int Id { get; set; }
        public string CodigoProducto { get; set; } = string.Empty;
        public decimal PrecioSinImpuestos { get; set; }
        public decimal PrecioFinal { get; set; }
        public decimal Impuestos { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCompra { get; set; }
        public string? Observaciones { get; set; }
    }
}
