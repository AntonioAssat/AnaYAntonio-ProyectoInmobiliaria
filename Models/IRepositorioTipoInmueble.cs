using AnaYAntonio_ProyectoInmobiliaria.Models;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public interface IRepositorioTipoInmueble : IRepositorio<TipoInmueble>
    {
        IList<TipoInmueble> ObtenerListaPaginada(
            string? buscar,
            int pagina,
            int cantidadPorPagina);

        int ObtenerCantidad(string? buscar);
    }
}