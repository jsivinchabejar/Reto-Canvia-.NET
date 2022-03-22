using Canvia.Core.Entities;
using System.Threading.Tasks;

namespace Canvia.Infrastructure.Repositories.Interfaces
{
    public interface IProductoRepository
    {
        Task<Producto> ObtenerProducto(int idProducto);
    }
}
