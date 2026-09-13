using System.ComponentModel.DataAnnotations;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class LoginView
    {
        [Required(ErrorMessage = "Debe ingresar su email.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Debe ingresar un email válido.")]
        public string Usuario { get; set; } = "";

        [Required(ErrorMessage = "Debe ingresar su contraseña.")]
        [DataType(DataType.Password)]
        public string Clave { get; set; } = "";
    }
}