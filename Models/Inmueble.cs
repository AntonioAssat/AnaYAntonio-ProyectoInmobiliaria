using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class Inmueble
    {
        [Key]
        public int ID_inmueble { get; set; }

        [Required]
        public Propietario Duenio { get; set; }

        [Required]
        [RegularExpression(
            @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s.,°#\-]+$",
            ErrorMessage = "La dirección contiene caracteres no válidos."
        )]
        public string Direccion { get; set; } = "";

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El cupo debe ser mayor a 0.")]
        public int Cupo { get; set; }

        [Required]
        public TipoInmueble Tipo { get; set; }

        [Required]
        [Range(-180, 180, ErrorMessage = "Las coordenadas deben estar entre -180 y 180.")]
        public decimal Coordenadas { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio por día debe ser mayor a 0.")]
        public decimal PrecioPorDia { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100.")]
        public decimal PorcentajeReserva { get; set; }

        public IFormFile? PortadaFile { get; set; }

        [ForeignKey(nameof(Imagen.InmuebleId))]
        public IList<Imagen> Imagenes { get; set; } = new List<Imagen>();

        public bool Estado { get; set; }
    }
}