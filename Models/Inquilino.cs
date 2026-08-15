namespace AnaYAntonio_ProyectoInmoviliaria.Models
{
    public class Inquilino
    {
        public int ID_inquilino{get; set; }
        public string NombreCompleto{get; set;}
        public string DNI{get; set; }
        
        public string Telefono{get; set; }
        public string Mail{get; set; }
        public bool Estado{get; set; }
    }
}