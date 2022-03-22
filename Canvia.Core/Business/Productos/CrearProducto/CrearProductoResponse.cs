using Canvia.Core.Entities;
using Services.API;

namespace Canvia.Core.Business.Productos.CrearProducto
{
    public class CrearProductoResponse : Result
    {
        public int IdProducto { get; set; }

        public CrearProductoResponse(Producto producto)
        {
            IdProducto = producto.IdProducto;
        }
    }
}
