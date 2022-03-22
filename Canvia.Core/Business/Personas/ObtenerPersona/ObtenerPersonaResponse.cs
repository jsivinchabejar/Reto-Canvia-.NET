using Canvia.Core.Entities;
using Services.API;

namespace Canvia.Core.Business.Personas.ObtenerPersona
{
    public class ObtenerPersonaResponse : Result
    {
        public int IdPersona { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NumeroDni { get; set; }

        public ObtenerPersonaResponse(Persona persona)
        {
            IdPersona = persona.IdPersona;
            Nombres = persona.Nombres;
            Apellidos = persona.Apellidos;
            NumeroDni = persona.NumeroDni;
        }
    }
}
