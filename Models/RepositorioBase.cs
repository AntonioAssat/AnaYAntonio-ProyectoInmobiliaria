using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
public abstract class RepositorioBase
{
    protected readonly IConfiguration configuration;
    protected readonly string connectionString;
    
    public RepositorioBase(IConfiguration configuration)
        {
            this.configuration=configuration;
            connectionString=configuration["ConnectionStrings:DefaultConnection"];
        }
    protected MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(connectionString);
        }    
}
}