using Canvia.Core.Entities;
using Services.API;

namespace Canvia.Core.Business.Productos.ActualizarProducto
{
    public class ActualizarProductoUseCase
    {
        private readonly ActualizarProductoRequest request;
        private readonly Producto producto;

        public ActualizarProductoUseCase(ActualizarProductoRequest request, Producto producto)
        {
            this.request = request;
            this.producto = producto;
        }

        public Result Execute()
        {
            Result result = Producto.ValidarProducto(producto);
            if (result.Code == Result.OK)
            {
                result = producto.Actualizar(request.Nombre, request.Descripcion, request.PrecioOriginal, request.PrecioPromocional);
            }
            return result;
        }
    }
}
