using System.ComponentModel.DataAnnotations;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class TipoInmueble
    {
        [Key]
        public int ID_tipo { get; set; }

        [Required]
        public string Nombre { get; set; } 
    }
}