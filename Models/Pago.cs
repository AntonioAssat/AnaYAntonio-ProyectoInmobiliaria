using System;
using System.ComponentModel.DataAnnotations;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class Pago
    {
        [Key]
        public int ID_pago { get; set; }

        [Required]
        public int ID_reserva { get; set; }

        [Required(ErrorMessage = "El concepto es obligatorio.")]
        public string Concepto { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaPago { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal Monto { get; set; }

        public bool Estado { get; set; } = true;
    }
}