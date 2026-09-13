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
        public string Direccion { get; set; } = "";

        [Required]
        public int Cupo { get; set; }

        [Required]
        public TipoInmueble Tipo { get; set; }

        [Required]
        public decimal Coordenadas { get; set; }

        [Required]
        public decimal PrecioPorDia { get; set; }

        [Required]
        public decimal PorcentajeReserva { get; set; }

        public IFormFile? PortadaFile { get; set; }

        [ForeignKey(nameof(Imagen.InmuebleId))]
        public IList<Imagen> Imagenes { get; set; } = new List<Imagen>();

        public bool Estado { get; set; }
    }
}
