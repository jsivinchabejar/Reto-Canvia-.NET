namespace Canvia.Core.Business.Productos.ListarProductos
{
    public class ListarProductosRequest
    {
        public string Busqueda { get; set; }
        public int CantidadPagina { get; set; } = 25;
        public int Pagina { get; set; } = 1;
    }
}
