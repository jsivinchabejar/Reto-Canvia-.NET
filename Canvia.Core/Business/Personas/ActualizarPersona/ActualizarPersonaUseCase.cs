using Canvia.Core.Entities;
using Services.API;

namespace Canvia.Core.Business.Personas.ActualizarPersona
{
    public class ActualizarPersonaUseCase
    {
        private readonly ActualizarPersonaRequest request;
        private readonly Persona persona;

        public ActualizarPersonaUseCase(ActualizarPersonaRequest request, Persona persona)
        {
            this.request = request;
            this.persona = persona;
        }

        public Result Execute()
        {
            Result result = Persona.ValidarPersona(persona);
            if (result.Code == Result.OK)
            {
                result = persona.Actualizar(request.Nombres, request.Apellidos, request.NumeroDni);
            }
            return result;
        }
    }
}
