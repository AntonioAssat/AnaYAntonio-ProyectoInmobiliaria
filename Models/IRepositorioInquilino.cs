using System.Collections.Generic;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public interface IRepositorioInquilino : IRepositorio<Inquilino>
    {
        IList<Inquilino> ObtenerLista();
        Inquilino? ObtenerPorId(int id);
    }
}