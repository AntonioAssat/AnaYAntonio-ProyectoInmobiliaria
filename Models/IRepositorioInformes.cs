using System;
using System.Collections.Generic;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public interface IRepositorioInformes
    {
        IList<Inmueble> ObtenerInmueblesPorPropietario(int idPropietario);
        IList<Inmueble> ObtenerInmueblesMasReservados(DateTime desde);
        IList<Inmueble> ObtenerInmueblesSinReservas(DateTime desde);
        IList<Reserva> ObtenerReservasVigentes(DateTime fechaInicio, DateTime fechaFin);
        IList<Reserva> ObtenerReservasQueTerminan(DateTime hasta);
        IList<Pago> ObtenerPagosPorReserva(int idReserva);
        IList<Inmueble> ObtenerInmueblesDisponibles(DateTime fechaInicio, DateTime fechaFin);
        IList<Reserva> BuscarReservas(string texto);
    }
}