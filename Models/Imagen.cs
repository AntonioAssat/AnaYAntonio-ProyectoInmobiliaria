using System.ComponentModel.DataAnnotations.Schema;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    [NotMapped]
    public class Imagen
    {
        public int Id { get; set; }

        public int InmuebleId { get; set; }

        public string Url { get; set; } = "";

        public IFormFile? Archivo { get; set; } = null;
    }
}