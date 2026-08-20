using System.ComponentModel.DataAnnotations;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class Inquilino
    {
        [Key]
        [Display(Name = "Código")]
        public int ID_inquilino { get; set; }

        [Required]
        public string NombreCompleto { get; set; }

        [Required]
        public string DNI { get; set; }

        public string Telefono { get; set; }

        [Required, EmailAddress]
        public string Mail { get; set; }

        public bool Estado { get; set; }
    }
}