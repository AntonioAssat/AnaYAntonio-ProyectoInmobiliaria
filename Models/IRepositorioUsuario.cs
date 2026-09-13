using System.Collections.Generic;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public interface IRepositorioUsuario
    {
        int Alta(Usuario usuario);

        int Baja(int id);

        int Modificacion(Usuario usuario);

        IList<Usuario> ObtenerLista();

        Usuario? ObtenerPorId(int id);

        Usuario? ObtenerPorEmail(string email);

        int AltaEstado(int id);
    }
}