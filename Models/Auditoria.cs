using System;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class Auditoria
    {
        public int ID_auditoria { get; set; }

        public int ID_usuario { get; set; }

        public string Entidad { get; set; } = string.Empty;

        public int ID_entidad { get; set; }

        public string Accion { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

        // Se utiliza para mostrar quién realizó la acción
        public string NombreUsuario { get; set; } = string.Empty;
    }
}