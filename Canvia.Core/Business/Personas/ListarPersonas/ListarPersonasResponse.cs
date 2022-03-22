using Canvia.Core.Entities;
using Services.API;
using System;
using System.Collections.Generic;
using Util.Entities;

namespace Canvia.Core.Business.Personas.ListarPersonas
{
    public class ListarPersonasResponse : Result
    {
        public List<PersonaResponse> Personas { get; set; }
        public int Paginas { get; set; }


        public ListarPersonasResponse(IEnumerable<Persona> personas, ValueContainer totalPages, int cantidadPagina)
        {
            Personas = new();
            foreach (var persona in personas)
                Personas.Add(new(persona));

            int total = (int?)totalPages?.Value ?? 1;
            Paginas = (int)Math.Ceiling(total / (double)cantidadPagina);
        }

        public class PersonaResponse
        {
            public int IdPersona { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string NumeroDni { get; set; }

            public PersonaResponse(Persona persona)
            {
                IdPersona = persona.IdPersona;
                Nombres = persona.Nombres;
                Apellidos = persona.Apellidos;
                NumeroDni = persona.NumeroDni;
            }
        }
    }
}
