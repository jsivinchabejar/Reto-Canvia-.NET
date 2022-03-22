using Canvia.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Entities;

namespace Canvia.Infrastructure.Queries.Interfaces
{
    public interface IPersonaQuery
    {
        Task<IEnumerable<Persona>> ListarPersonas(string busqueda = null, int? pagina = null, int? cantidadPagina = null, ValueContainer totalPages = null);
    }
}
