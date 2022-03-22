using Canvia.Core.Entities;
using Services.API;

namespace Canvia.Core.Business.Personas.CrearPersona
{
    public class CrearPersonaUseCase
    {
        private readonly CrearPersonaRequest request;
        private readonly Persona personaExistenteDNI;
        public Persona PersonaNueva { get; set; }

        public CrearPersonaUseCase(CrearPersonaRequest request, Persona personaExistenteDNI)
        {
            this.request = request;
            this.personaExistenteDNI = personaExistenteDNI;
        }

        public Result Execute()
        {
            Result result = Persona.ValidarPersonaExistente(personaExistenteDNI);
            if (result.Code == Result.OK)
            {
                result = ValidarRequest();
                if (result.Code == Result.OK)
                {
                    PersonaNueva = new(request.Nombres, request.Apellidos, request.NumeroDni);
                }
            }
            return result;
        }

        private Result ValidarRequest()
        {
            if (string.IsNullOrWhiteSpace(request.Nombres) || string.IsNullOrWhiteSpace(request.Apellidos) || string.IsNullOrWhiteSpace(request.NumeroDni))
                return new() { Code = Result.BAD_REQUEST, Type = "datos_incorrecos", Message = "Complete los datos obligatorios." };

            return new();
        }
    }
}
