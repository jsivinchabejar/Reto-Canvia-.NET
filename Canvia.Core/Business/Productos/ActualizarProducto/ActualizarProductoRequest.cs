namespace Canvia.Core.Business.Productos.ActualizarProducto
{
    public class ActualizarProductoRequest
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal? PrecioPromocional { get; set; }
    }
}
