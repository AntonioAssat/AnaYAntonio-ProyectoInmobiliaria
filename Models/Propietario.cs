using System.ComponentModel.DataAnnotations;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class Propietario
    {
        [Key]
        public int ID_propietario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [RegularExpression(
            @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
            ErrorMessage = "El nombre solo puede contener letras."
        )]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [RegularExpression(
            @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
            ErrorMessage = "El apellido solo puede contener letras."
        )]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio")]
        [RegularExpression(
            @"^[0-9]+$",
            ErrorMessage = "El DNI solo puede contener números."
        )]
        public string DNI { get; set; }

        [RegularExpression(
            @"^[0-9+\-\s()]+$",
            ErrorMessage = "El teléfono contiene caracteres no válidos."
        )]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El mail es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un mail válido")]
        public string Mail { get; set; }

        public bool Estado { get; set; }
    }
}