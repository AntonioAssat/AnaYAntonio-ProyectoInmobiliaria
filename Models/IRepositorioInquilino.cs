using System.Collections.Generic;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public interface IRepositorioInquilino : IRepositorio<Inquilino>
    {
        IList<Inquilino> ObtenerListaPaginada(
            string? buscar,
            int pagina,
            int cantidadPorPagina);

        int ObtenerCantidad(string? buscar);
    }
}