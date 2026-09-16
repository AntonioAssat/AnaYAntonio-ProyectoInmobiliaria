namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public interface IRepositorioReserva : IRepositorio<Reserva>
    {
        bool ExisteReservaSuperpuesta(Reserva reserva);

        bool ExisteReservaSuperpuesta(Reserva reserva, int idReservaExcluir);

        int FinalizarAnticipadamente(
            int idReserva,
            DateTime fechaFinEfectiva);

        IList<Reserva> ObtenerListaPaginada(
            string? buscar,
            int pagina,
            int cantidadPorPagina);

        int ObtenerCantidad(string? buscar);
    }
}