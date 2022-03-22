using Canvia.Core.Business.Productos.ActualizarProducto;
using Canvia.Core.Business.Productos.CrearProducto;
using Canvia.Core.Business.Productos.ListarProductos;
using Canvia.Core.Business.Productos.ObtenerProducto;
using Canvia.Core.Entities;
using Canvia.Infrastructure.Queries.Interfaces;
using Canvia.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.API;
using System.Threading.Tasks;
using Util.Entities;

namespace Canvia.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductosController : BaseController
    {
        private readonly IProductoQuery productoQuery;
        private readonly IProductoRepository productoRepository;
        private readonly IBaseRepository baseRepository;

        public ProductosController(
            IProductoQuery productoQuery,
            IProductoRepository productoRepository,
            IBaseRepository baseRepository)
        {
            this.productoQuery = productoQuery;
            this.productoRepository = productoRepository;
            this.baseRepository = baseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarProductos([FromQuery] ListarProductosRequest request)
        {
            //obtenemos el estado
            var totalPages = new ValueContainer();
            var productos = await productoQuery.ListarProductos(request.Busqueda, request.Pagina, request.CantidadPagina, totalPages);

            //retornamos la respuesta
            return ResultResponse(new ListarProductosResponse(productos, totalPages, request.CantidadPagina));
        }

        [HttpGet("{idProducto}")]
        public async Task<IActionResult> ObtenerProducto(int idProducto)
        {
            //obtenemos el estado
            var producto = await productoRepository.ObtenerProducto(idProducto);
            var result = Producto.ValidarProducto(producto);
            if (result.Code == Result.OK)
                result = new ObtenerProductoResponse(producto);

            //retornamos la respuesta
            return ResultResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] CrearProductoRequest request)
        {
            //procesamos la lógica
            var useCase = new CrearProductoUseCase(request);
            var result = useCase.Execute();

            //guardamos los cambios
            if (result.Code == Result.OK)
            {
                //creamos al producto
                await baseRepository.AddAsync(useCase.ProductoNuevo);

                await baseRepository.SaveChangesAsync();

                result = new CrearProductoResponse(useCase.ProductoNuevo);
            }

            //retornamos la respuesta
            return ResultResponse(result);
        }

        [HttpPut("{idProducto}")]
        public async Task<IActionResult> ActualizarProducto(int idProducto, [FromBody] ActualizarProductoRequest request)
        {
            //obtenemos el estado
            var producto = await productoRepository.ObtenerProducto(idProducto);

            //procesamos la lógica
            var result = new ActualizarProductoUseCase(request, producto).Execute();

            //guardamos los cambios
            if (result.Code == Result.OK)
                await baseRepository.SaveChangesAsync();

            //retornamos la respuesta
            return ResultResponse(result);
        }

        [HttpDelete("{idProducto}")]
        public async Task<IActionResult> EliminarProducto(int idProducto)
        {
            //obtenemos el estado
            var producto = await productoRepository.ObtenerProducto(idProducto);

            //procesamos la lógica
            var result = Producto.ValidarProducto(producto);

            //guardamos los cambios
            if (result.Code == Result.OK)
            {
                producto.Eliminar();
                await baseRepository.SaveChangesAsync();
            }

            //retornamos la respuesta
            return ResultResponse(result);
        }
    }
}
