namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    

using System.ComponentModel.DataAnnotations;

public class Propietario
{
    [Key]
    public int ID_propietario{get; set; }
    [Required]
    public string DNI{get; set; }
    [Required]
    public string Nombre{get; set; }
    public string Telefono{get; set; }
    [Required, EmailAddress]
    public string Mail{get; set; }

}
}    
