using MySql.Data.MySqlClient;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class RepositorioTipoInmueble : RepositorioBase
    {
        public RepositorioTipoInmueble(IConfiguration configuration)
            : base(configuration)
        {
        }

        public int Alta(TipoInmueble tipo)
        {
            int res = -1;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"INSERT INTO TipoInmueble (Nombre, Estado)
                               VALUES (@Nombre, @Estado);
                               SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", tipo.Nombre);
                    command.Parameters.AddWithValue("@Estado", tipo.Estado);

                    res = Convert.ToInt32(command.ExecuteScalar());
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

                string sql = @"UPDATE TipoInmueble
                               SET Estado = false
                               WHERE ID_tipo = @Id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    res = command.ExecuteNonQuery();
                }
            }

            return res;
        }

        public int AltaEstado(int id)
        {
            int res = -1;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"UPDATE TipoInmueble
                               SET Estado = true
                               WHERE ID_tipo = @Id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    res = command.ExecuteNonQuery();
                }
            }

            return res;
        }

        public int Modificacion(TipoInmueble tipo)
        {
            int res = -1;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"UPDATE TipoInmueble
                               SET Nombre = @Nombre
                               WHERE ID_tipo = @Id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", tipo.Nombre);
                    command.Parameters.AddWithValue("@Id", tipo.ID_tipo);

                    res = command.ExecuteNonQuery();
                }
            }

            return res;
        }

        public IList<TipoInmueble> ObtenerLista(int paginaNro = 1, int tamPagina = 10)
        {
            var lista = new List<TipoInmueble>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"SELECT ID_tipo, Nombre, Estado
                               FROM TipoInmueble
                               ORDER BY Nombre
                               LIMIT @Cantidad OFFSET @Offset";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Cantidad", tamPagina);
                    command.Parameters.AddWithValue("@Offset", (paginaNro - 1) * tamPagina);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new TipoInmueble
                            {
                                ID_tipo = reader.GetInt32("ID_tipo"),
                                Nombre = reader.GetString("Nombre"),
                                Estado = reader.GetBoolean("Estado")
                            });
                        }
                    }
                }
            }

            return lista;
        }


        public TipoInmueble? ObtenerPorId(int id)
        {
            TipoInmueble? tipo = null;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"SELECT ID_tipo, Nombre, Estado
                               FROM TipoInmueble
                               WHERE ID_tipo = @Id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tipo = new TipoInmueble
                            {
                                ID_tipo = reader.GetInt32("ID_tipo"),
                                Nombre = reader.GetString("Nombre"),
                                Estado = reader.GetBoolean("Estado")
                            };
                        }
                    }
                }
            }

            return tipo;
        }
    }
}