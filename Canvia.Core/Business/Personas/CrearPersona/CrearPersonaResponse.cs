using Canvia.Core.Entities;
using Services.API;

namespace Canvia.Core.Business.Personas.CrearPersona
{
    public class CrearPersonaResponse : Result
    {
        public int IdPersona { get; set; }

        public CrearPersonaResponse(Persona persona)
        {
            IdPersona = persona.IdPersona;
        }
    }
}
