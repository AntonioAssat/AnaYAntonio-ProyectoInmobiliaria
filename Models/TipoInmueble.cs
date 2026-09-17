using System.ComponentModel.DataAnnotations;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class TipoInmueble
    {
        [Key]
        public int ID_tipo { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
            ErrorMessage = "El nombre solo puede contener letras.")]
        public string Nombre { get; set; } 

        [Required]
        public bool Estado { get; set; }
    }
}