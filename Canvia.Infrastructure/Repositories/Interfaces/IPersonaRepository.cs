using Canvia.Core.Entities;
using System.Threading.Tasks;

namespace Canvia.Infrastructure.Repositories.Interfaces
{
    public interface IPersonaRepository
    {
        Task<Persona> ObtenerPersonaPorDNI(string numeroDni);
        Task<Persona> ObtenerPersona(int idPersona);
    }
}
