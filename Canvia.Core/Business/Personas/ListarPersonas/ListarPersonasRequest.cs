namespace Canvia.Core.Business.Personas.ListarPersonas
{
    public class ListarPersonasRequest
    {
        public string Busqueda { get; set; }
        public int CantidadPagina { get; set; } = 25;
        public int Pagina { get; set; } = 1;
    }
}
