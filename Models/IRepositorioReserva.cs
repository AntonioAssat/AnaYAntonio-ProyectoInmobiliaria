namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public interface IRepositorioReserva : IRepositorio<Reserva>
    {
        bool ExisteReservaSuperpuesta(Reserva reserva);

        bool ExisteReservaSuperpuesta(Reserva reserva, int idReservaExcluir);
    }
}