using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class RepositorioImagen : RepositorioBase, IRepositorioImagen
    {
        public RepositorioImagen(IConfiguration configuration) : base(configuration)
        {
        }

        public int Alta(Imagen imagen)
        {
            int res = -1;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var sql = @"INSERT INTO Imagenes (InmuebleId, Url)
                            VALUES (@InmuebleId, @Url);";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@InmuebleId", imagen.InmuebleId);
                    command.Parameters.AddWithValue("@Url", imagen.Url);

                    res = command.ExecuteNonQuery();
                }
            }

            return res;
        }

        public int Baja(int id)
        {
            int res = -1;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var sql = @"DELETE FROM Imagenes
                            WHERE Id = @Id;";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    res = command.ExecuteNonQuery();
                }
            }

            return res;
        }

        public int Modificacion(Imagen imagen)
        {
            int res = -1;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var sql = @"UPDATE Imagenes
                            SET Url = @Url
                            WHERE Id = @Id;";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Url", imagen.Url);
                    command.Parameters.AddWithValue("@Id", imagen.Id);

                    res = command.ExecuteNonQuery();
                }
            }

            return res;
        }

        public IList<Imagen> ObtenerLista()
        {
            var lista = new List<Imagen>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var sql = @"SELECT Id, InmuebleId, Url
                            FROM Imagenes;";

                using (var command = new MySqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Imagen
                        {
                            Id = reader.GetInt32("Id"),
                            InmuebleId = reader.GetInt32("InmuebleId"),
                            Url = reader.GetString("Url")
                        });
                    }
                }
            }

            return lista;
        }

        public int ObtenerCantidad()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var sql = "SELECT COUNT(*) FROM Imagenes;";

                using (var command = new MySqlCommand(sql, connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public Imagen ObtenerPorId(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var sql = @"SELECT Id, InmuebleId, Url
                            FROM Imagenes
                            WHERE Id = @Id;";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Imagen
                            {
                                Id = reader.GetInt32("Id"),
                                InmuebleId = reader.GetInt32("InmuebleId"),
                                Url = reader.GetString("Url")
                            };
                        }
                    }
                }
            }

            return null!;
        }

        public IList<Imagen> BuscarPorInmueble(int inmuebleId)
        {
            var lista = new List<Imagen>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var sql = @"SELECT Id, InmuebleId, Url
                            FROM Imagenes
                            WHERE InmuebleId = @InmuebleId;";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@InmuebleId", inmuebleId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Imagen
                            {
                                Id = reader.GetInt32("Id"),
                                InmuebleId = reader.GetInt32("InmuebleId"),
                                Url = reader.GetString("Url")
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}