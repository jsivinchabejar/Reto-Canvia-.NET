using Canvia.Core.Entities;
using Canvia.Infrastructure.Data;
using Canvia.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Canvia.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly RetoCanviaContext context;

        public ProductoRepository(RetoCanviaContext context)
        {
            this.context = context;
        }

        public async Task<Producto> ObtenerProducto(int idProducto)
        {
            return await context.Productos.FirstOrDefaultAsync(p => p.IdProducto == idProducto);
        }
    }
}
