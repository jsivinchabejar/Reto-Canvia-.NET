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
    public class PersonaQuery : IPersonaQuery
    {
        private readonly RetoCanviaContext context;

        public PersonaQuery(RetoCanviaContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Persona>> ListarPersonas(string busqueda = null, int? pagina = null, int? cantidadPagina = null, ValueContainer totalPages = null)
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

                var result = await connection.QueryAsync<Persona>("sp_ListarPersonas",
                 param: parameters,
                 commandType: CommandType.StoredProcedure);

                totalPages.Value = parameters.Get<int>("@Total");

                return result;
            };
        }
    }
}
