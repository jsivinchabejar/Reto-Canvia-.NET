using Canvia.Core.Entities;
using Services.API;

namespace Canvia.Core.Business.Productos.CrearProducto
{
    public class CrearProductoUseCase
    {
        private readonly CrearProductoRequest request;
        public Producto ProductoNuevo { get; set; }

        public CrearProductoUseCase(CrearProductoRequest request)
        {
            this.request = request;
        }

        public Result Execute()
        {
            Result result = ValidarRequest();
            if (result.Code == Result.OK)
            {
                ProductoNuevo = new(request.Nombre, request.Descripcion, request.PrecioOriginal, request.PrecioPromocional);
            }
            return result;
        }

        private Result ValidarRequest()
        {
            if (string.IsNullOrWhiteSpace(request.Nombre) || string.IsNullOrWhiteSpace(request.Descripcion) || request.PrecioOriginal <= 0 || (request.PrecioPromocional != null && request.PrecioPromocional <= 0))
                return new() { Code = Result.BAD_REQUEST, Type = "datos_incorrecos", Message = "Complete los datos obligatorios." };

            return new();
        }
    }
}
