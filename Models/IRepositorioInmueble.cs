namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public interface IRepositorioInmueble : IRepositorio<Inmueble>
    {
        IList<Inmueble> ObtenerListaPaginada(
            string? buscar,
            int pagina,
            int cantidadPorPagina);

        int ObtenerCantidad(string? buscar);
    }
}