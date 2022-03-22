using Services.API;
using System;

#nullable disable

namespace Canvia.Core.Entities
{
    public partial class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal? PrecioPromocional { get; set; }
        public bool Eliminado { get; set; }
        public DateTime FechaHoraRegistro { get; set; }
        public DateTime FechaHoraActualizacion { get; set; }

        public Producto()
        {

        }

        public Producto(string nombre, string descripcion, decimal precioOriginal, decimal? precioPromocional)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            PrecioOriginal = precioOriginal;
            PrecioPromocional = precioPromocional;
        }

        public Result Actualizar(string nombre, string descripcion, decimal precioOriginal, decimal? precioPromocional)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion) || precioOriginal <= 0 || (precioPromocional != null && precioPromocional <= 0))
                return new() { Code = Result.BAD_REQUEST, Type = "datos_incorrecos", Message = "Complete los datos obligatorios." };

            Nombre = nombre;
            Descripcion = descripcion;
            PrecioOriginal = precioOriginal;
            PrecioPromocional = precioPromocional;

            return new();
        }

        public static Result ValidarProducto(Producto producto)
        {
            if (producto == null)
                return new() { Code = Result.NOT_FOUND, Type = "producto_inexistente", Message = "No se encontró el producto solicitado." };

            return new();
        }

        public void Eliminar()
        {
            Eliminado = true;
        }
    }
}
