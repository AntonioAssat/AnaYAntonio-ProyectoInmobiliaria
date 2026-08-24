using System.ComponentModel.DataAnnotations;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class Inquilino
    {
        [Key]
        [Display(Name = "Código")]
        public int ID_inquilino { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
            ErrorMessage = "El nombre solo puede contener letras.")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
            ErrorMessage = "El apellido solo puede contener letras.")]
        public string Apellido { get; set; } = "";

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [RegularExpression(@"^\d+$",
            ErrorMessage = "El DNI solo puede contener números.")]
        public string DNI { get; set; } = "";

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\d+$",
            ErrorMessage = "El teléfono solo puede contener números.")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        public string Mail { get; set; } = "";

        public bool Estado { get; set; }
    }
}