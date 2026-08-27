using System.ComponentModel.DataAnnotations;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class Propietario
    {
        [Key]
        public int ID_propietario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio")]
        public string DNI { get; set; }

        public string Telefono { get; set; }

        [Required(ErrorMessage = "El mail es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un mail válido")]
        public string Mail { get; set; }

        public bool Estado { get; set; }
    }
}