using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class RepositorioPropietario : RepositorioBase, IRepositorioPropietario
    {
        public RepositorioPropietario(IConfiguration configuration)
            : base(configuration)
        {
        }
        public int Alta(Propietario p)
        {
            using (var connection = ObtenerConexion())
            {
                connection.Open();
                var sql = @"INSERT INTO Propietario 
                            (Nombre, DNI, Telefono, Mail, Estado)
                            VALUES (@Nombre, @DNI, @Telefono, @Mail, @Estado);";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", p.NombreCompleto);
                    command.Parameters.AddWithValue("@DNI", p.DNI);
                    command.Parameters.AddWithValue("@Telefono", p.Telefono);
                    command.Parameters.AddWithValue("@Mail", p.Mail);
                    command.Parameters.AddWithValue("@Estado", true);

                    return command.ExecuteNonQuery();
                }
            }
        }

        public int Baja(int id)
        {
            using (var connection = ObtenerConexion())
            {
                connection.Open();

                var sql = @"UPDATE Propietario
                    SET Estado = false
                    WHERE id_propietario = @id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    return command.ExecuteNonQuery();
                }
            }
        }

        public int Modificacion(Propietario p)
        {
            using (var connection = ObtenerConexion())
            {
                connection.Open();

                var sql = @"UPDATE Propietario
                    SET Nombre = @Nombre,
                        DNI = @DNI,
                        Telefono = @Telefono,
                        Mail = @Mail,
                        Estado = @Estado
                    WHERE id_propietario = @id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", p.NombreCompleto);
                    command.Parameters.AddWithValue("@DNI", p.DNI);
                    command.Parameters.AddWithValue("@Telefono", p.Telefono);
                    command.Parameters.AddWithValue("@Mail", p.Mail);
                    command.Parameters.AddWithValue("@Estado", p.Estado);
                    command.Parameters.AddWithValue("@id", p.ID_propietario);

                    return command.ExecuteNonQuery();
                }
            }
        }

        public IList<Propietario> ObtenerLista()
        {
            var lista = new List<Propietario>();

            using (var connection = ObtenerConexion())
            {
                connection.Open();

                var sql = @"SELECT id_propietario, Nombre, DNI, Telefono, Mail, Estado
                    FROM Propietario";

                using (var command = new MySqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Propietario
                        {
                            ID_propietario = reader.GetInt32("id_propietario"),
                            NombreCompleto = reader.GetString("Nombre"),
                            DNI = reader.GetString("DNI"),
                            Telefono = reader.GetString("Telefono"),
                            Mail = reader.GetString("Mail"),
                            Estado = reader.GetBoolean("Estado")
                        });
                    }
                }
            }

            return lista;
        }
    }
}