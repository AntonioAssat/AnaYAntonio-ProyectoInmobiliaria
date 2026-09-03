using System.ComponentModel.DataAnnotations;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class Reserva
    {
        [Key]
        public int ID_reserva { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un inquilino.")]
        public int ID_inquilino { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un inmueble.")]
        public int ID_inmueble { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de finalización es obligatoria.")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El monto por día es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto por día debe ser mayor a 0.")]
        public decimal MontoPorDia { get; set; }

        public bool Estado { get; set; }
    }
}