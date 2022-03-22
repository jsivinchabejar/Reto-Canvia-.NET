using Canvia.Core.Entities;
using Services.API;
using System;
using System.Collections.Generic;
using Util.Entities;

namespace Canvia.Core.Business.Productos.ListarProductos
{
    public class ListarProductosResponse : Result
    {
        public List<ProductoResponse> Productos { get; set; }
        public int Paginas { get; set; }


        public ListarProductosResponse(IEnumerable<Producto> productos, ValueContainer totalPages, int cantidadPagina)
        {
            Productos = new();
            foreach (var producto in productos)
                Productos.Add(new(producto));

            int total = (int?)totalPages?.Value ?? 1;
            Paginas = (int)Math.Ceiling(total / (double)cantidadPagina);
        }

        public class ProductoResponse
        {
            public int IdProducto { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public decimal PrecioOriginal { get; set; }
            public decimal? PrecioPromocional { get; set; }

            public ProductoResponse(Producto producto)
            {
                IdProducto = producto.IdProducto;
                Nombre = producto.Nombre;
                Descripcion = producto.Descripcion;
                PrecioOriginal = producto.PrecioOriginal;
                PrecioPromocional = producto.PrecioPromocional;
            }
        }

    }
}
