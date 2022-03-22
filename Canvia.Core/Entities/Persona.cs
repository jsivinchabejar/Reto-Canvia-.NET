using Services.API;
using System;

#nullable disable

namespace Canvia.Core.Entities
{
    public partial class Persona
    {
        public int IdPersona { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NumeroDni { get; set; }
        public bool Eliminado { get; set; }
        public DateTime FechaHoraRegistro { get; set; }
        public DateTime FechaHoraActualizacion { get; set; }

        public Persona()
        {

        }

        public Persona(string nombres, string apellidos, string numeroDni)
        {
            Nombres = nombres;
            Apellidos = apellidos;
            NumeroDni = numeroDni;
        }

        public static Result ValidarPersona(Persona persona)
        {
            if (persona == null)
                return new() { Code = Result.NOT_FOUND, Type = "persona_inexistente", Message = "No se encontró la persona solicitada." };

            return new();
        }

        public Result Actualizar(string nombres, string apellidos, string numeroDni)
        {
            if (string.IsNullOrWhiteSpace(nombres) || string.IsNullOrWhiteSpace(apellidos) || string.IsNullOrWhiteSpace(numeroDni))
                return new() { Code = Result.BAD_REQUEST, Type = "datos_incorrecos", Message = "Complete los datos obligatorios." };

            Nombres = nombres;
            Apellidos = apellidos;
            NumeroDni = numeroDni;

            return new();
        }

        public static Result ValidarPersonaExistente(Persona persona)
        {
            if (persona != null)
                return new() { Code = Result.UNPROCESSABLE_ENTITY, Type = "persona_existente", Message = "Ya existe una persona con el mismo número DNI." };

            return new();
        }

        public void Eliminar()
        {
            Eliminado = true;
        }
    }
}
