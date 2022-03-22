using Canvia.Core.Entities;
using Services.API;

namespace Canvia.Core.Business.Productos.ObtenerProducto
{
    public class ObtenerProductoResponse : Result
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal? PrecioPromocional { get; set; }

        public ObtenerProductoResponse(Producto producto)
        {
            IdProducto = producto.IdProducto;
            Nombre = producto.Nombre;
            Descripcion = producto.Descripcion;
            PrecioOriginal = producto.PrecioOriginal;
            PrecioPromocional = producto.PrecioPromocional;
        }
    }
}
