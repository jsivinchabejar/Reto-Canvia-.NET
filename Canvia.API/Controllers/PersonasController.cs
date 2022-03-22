using Canvia.Core.Business.Personas.ActualizarPersona;
using Canvia.Core.Business.Personas.CrearPersona;
using Canvia.Core.Business.Personas.ListarPersonas;
using Canvia.Core.Business.Personas.ObtenerPersona;
using Canvia.Core.Entities;
using Canvia.Infrastructure.Queries.Interfaces;
using Canvia.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.API;
using System.Threading.Tasks;
using Util.Entities;

namespace Canvia.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonasController : BaseController
    {
        private readonly IPersonaQuery personaQuery;
        private readonly IPersonaRepository personaRepository;
        private readonly IBaseRepository baseRepository;

        public PersonasController(IPersonaQuery personaQuery,
            IPersonaRepository personaRepository,
            IBaseRepository baseRepository)
        {
            this.personaQuery = personaQuery;
            this.personaRepository = personaRepository;
            this.baseRepository = baseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarPersonas([FromQuery] ListarPersonasRequest request)
        {
            //obtenemos el estado
            var totalPages = new ValueContainer();
            var personas = await personaQuery.ListarPersonas(request.Busqueda, request.Pagina, request.CantidadPagina, totalPages);

            //retornamos la respuesta
            return ResultResponse(new ListarPersonasResponse(personas, totalPages, request.CantidadPagina));
        }

        [HttpGet("{idPersona}")]
        public async Task<IActionResult> ObtenerPersona(int idPersona)
        {
            //obtenemos el estado
            var persona = await personaRepository.ObtenerPersona(idPersona);
            var result = Persona.ValidarPersona(persona);
            if (result.Code == Result.OK)
                result = new ObtenerPersonaResponse(persona);

            //retornamos la respuesta
            return ResultResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> CrearPersona([FromBody] CrearPersonaRequest request)
        {
            //obtenemos el estado
            var personaExistenteDNI = await personaRepository.ObtenerPersonaPorDNI(request.NumeroDni);

            //procesamos la lógica
            var useCase = new CrearPersonaUseCase(request, personaExistenteDNI);
            var result = useCase.Execute();

            //guardamos los cambios
            if (result.Code == Result.OK)
            {
                //creamos a la persona
                await baseRepository.AddAsync(useCase.PersonaNueva);

                await baseRepository.SaveChangesAsync();

                result = new CrearPersonaResponse(useCase.PersonaNueva);
            }

            //retornamos la respuesta
            return ResultResponse(result);
        }

        [HttpPut("{idPersona}")]
        public async Task<IActionResult> ActualizarPersona(int idPersona, [FromBody] ActualizarPersonaRequest request)
        {
            //obtenemos el estado
            var persona = await personaRepository.ObtenerPersona(idPersona);

            //procesamos la lógica
            var result = new ActualizarPersonaUseCase(request, persona).Execute();

            //guardamos los cambios
            if (result.Code == Result.OK)
                await baseRepository.SaveChangesAsync();

            //retornamos la respuesta
            return ResultResponse(result);
        }

        [HttpDelete("{idPersona}")]
        public async Task<IActionResult> EliminarPersona(int idPersona)
        {
            //obtenemos el estado
            var persona = await personaRepository.ObtenerPersona(idPersona);

            //procesamos la lógica
            var result = Persona.ValidarPersona(persona);

            //guardamos los cambios
            if (result.Code == Result.OK)
            {
                persona.Eliminar();
                await baseRepository.SaveChangesAsync();
            }

            //retornamos la respuesta
            return ResultResponse(result);
        }
    }
}
