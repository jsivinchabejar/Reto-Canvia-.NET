using Canvia.Core.Entities;
using Canvia.Infrastructure.Data;
using Canvia.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Canvia.Infrastructure.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly RetoCanviaContext context;

        public PersonaRepository(RetoCanviaContext context)
        {
            this.context = context;
        }

        public async Task<Persona> ObtenerPersona(int idPersona)
        {
            return await context.Persona.FirstOrDefaultAsync(p => p.IdPersona == idPersona);
        }

        public async Task<Persona> ObtenerPersonaPorDNI(string numeroDni)
        {
            return await context.Persona.FirstOrDefaultAsync(p => p.NumeroDni == numeroDni);
        }
    }
}
