namespace Canvia.Core.Business.Productos.CrearProducto
{
    public class CrearProductoRequest
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal? PrecioPromocional { get; set; }
    }
}
