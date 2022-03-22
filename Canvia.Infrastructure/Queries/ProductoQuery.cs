using Canvia.Core.Entities;
using Canvia.Infrastructure.Data;
using Canvia.Infrastructure.Queries.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Util.Entities;

namespace Canvia.Infrastructure.Queries
{
    public class ProductoQuery : IProductoQuery
    {
        private readonly RetoCanviaContext context;

        public ProductoQuery(RetoCanviaContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Producto>> ListarProductos(string busqueda = null, int? pagina = null, int? cantidadPagina = null, ValueContainer totalPages = null)
        {
            using (var connection = context.Database.GetDbConnection())
            {
                var parameters = new DynamicParameters(new
                {
                    Busqueda = busqueda,
                    Pagina = pagina,
                    CantidadPagina = cantidadPagina
                });

                parameters.Add("Total", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = await connection.QueryAsync<Producto>("sp_ListarProductos",
                 param: parameters,
                 commandType: CommandType.StoredProcedure);

                totalPages.Value = parameters.Get<int>("@Total");

                return result;
            };
        }
    }
}
