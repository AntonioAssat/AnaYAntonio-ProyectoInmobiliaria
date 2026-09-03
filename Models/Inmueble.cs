using System.ComponentModel.DataAnnotations;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class Inmueble
    {
        [Key]
        public int ID_inmueble { get; set; }

        [Required]
        public Propietario Duenio { get; set; }

        [Required]
        public string Direccion { get; set; } = "";

        [Required]
        public int Cupo { get; set; }

        [Required]
        public TipoInmueble tipo { get; set; }

        [Required]
        public decimal Coordenadas { get; set; }

        [Required]
        public decimal PrecioPorDia { get; set; }

        [Required]
        public decimal PorcentajeReserva { get; set; }

        public bool Estado { get; set; }
    }
}
