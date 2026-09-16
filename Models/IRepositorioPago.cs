using System.Collections.Generic;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public interface IRepositorioPago
    {
        int Alta(Pago pago);

        int Baja(int id);

        int Modificacion(Pago pago);

        IList<Pago> ObtenerLista();

        Pago? ObtenerPorId(int id);

        int AltaEstado(int id);

        IList<Pago> ObtenerListaPaginada(
            string? buscar,
            int pagina,
            int cantidadPorPagina
        );

        int ObtenerCantidad(string? buscar);
    }
}