namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public interface IRepositorioPropietario : IRepositorio<Propietario>
    {
        IList<Propietario> ObtenerListaPaginada(
    string? buscar,
    int pagina,
    int cantidadPorPagina);

        int ObtenerCantidad(string? buscar);
    }

}