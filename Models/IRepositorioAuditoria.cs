using System.Collections.Generic;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public interface IRepositorioAuditoria
    {
        int Alta(Auditoria auditoria);
        IList<Auditoria> ObtenerLista();
        IList<Auditoria> ObtenerPorEntidad(string entidad, int idEntidad);
    }
}