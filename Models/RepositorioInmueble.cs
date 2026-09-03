using MySql.Data.MySqlClient;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class RepositorioInmueble : RepositorioBase, IRepositorioInmueble
    {
        public RepositorioInmueble(IConfiguration configuration)
            : base(configuration)
        {
        }

        public int Alta(Inmueble inmueble)
        {
            int res = -1;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"INSERT INTO Inmueble
                               (ID_propietario, Direccion, Cupo, ID_tipo,
                                Coordenadas, PrecioPorDia, PorcentajeReserva, Estado)
                               VALUES
                               (@ID_propietario, @Direccion, @Cupo, @ID_tipo,
                                @Coordenadas, @PrecioPorDia, @PorcentajeReserva, @Estado);
                               SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ID_propietario",
                        inmueble.Duenio.ID_propietario
                    );

                    command.Parameters.AddWithValue(
                        "@Direccion",
                        inmueble.Direccion
                    );

                    command.Parameters.AddWithValue(
                        "@Cupo",
                        inmueble.Cupo
                    );

                    command.Parameters.AddWithValue(
                        "@ID_tipo",
                        inmueble.Tipo.ID_tipo
                    );

                    command.Parameters.AddWithValue(
                        "@Coordenadas",
                        inmueble.Coordenadas
                    );

                    command.Parameters.AddWithValue(
                        "@PrecioPorDia",
                        inmueble.PrecioPorDia
                    );

                    command.Parameters.AddWithValue(
                        "@PorcentajeReserva",
                        inmueble.PorcentajeReserva
                    );

                    command.Parameters.AddWithValue(
                        "@Estado",
                        inmueble.Estado
                    );

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

                string sql = @"UPDATE Inmueble
                               SET Estado = false
                               WHERE ID_inmueble = @Id";

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

                string sql = @"UPDATE Inmueble
                               SET Estado = true
                               WHERE ID_inmueble = @Id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    res = command.ExecuteNonQuery();
                }
            }

            return res;
        }

        public int Modificacion(Inmueble inmueble)
        {
            int res = -1;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"UPDATE Inmueble
                               SET ID_propietario = @ID_propietario,
                                   Direccion = @Direccion,
                                   Cupo = @Cupo,
                                   ID_tipo = @ID_tipo,
                                   Coordenadas = @Coordenadas,
                                   PrecioPorDia = @PrecioPorDia,
                                   PorcentajeReserva = @PorcentajeReserva
                               WHERE ID_inmueble = @Id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ID_propietario",
                        inmueble.Duenio.ID_propietario
                    );

                    command.Parameters.AddWithValue(
                        "@Direccion",
                        inmueble.Direccion
                    );

                    command.Parameters.AddWithValue(
                        "@Cupo",
                        inmueble.Cupo
                    );

                    command.Parameters.AddWithValue(
                        "@ID_tipo",
                        inmueble.Tipo.ID_tipo
                    );

                    command.Parameters.AddWithValue(
                        "@Coordenadas",
                        inmueble.Coordenadas
                    );

                    command.Parameters.AddWithValue(
                        "@PrecioPorDia",
                        inmueble.PrecioPorDia
                    );

                    command.Parameters.AddWithValue(
                        "@PorcentajeReserva",
                        inmueble.PorcentajeReserva
                    );

                    command.Parameters.AddWithValue(
                        "@Id",
                        inmueble.ID_inmueble
                    );

                    res = command.ExecuteNonQuery();
                }
            }

            return res;
        }

        public IList<Inmueble> ObtenerLista()
        {
            var lista = new List<Inmueble>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"SELECT
                                   i.ID_inmueble,
                                   i.Direccion,
                                   i.Cupo,
                                   i.Coordenadas,
                                   i.PrecioPorDia,
                                   i.PorcentajeReserva,
                                   i.Estado,

                                   p.ID_propietario,
                                   p.Nombre AS NombrePropietario,
                                   p.Apellido AS ApellidoPropietario,
                                   p.DNI AS DNIPropietario,
                                   p.Telefono AS TelefonoPropietario,
                                   p.Mail AS MailPropietario,
                                   p.Estado AS EstadoPropietario,

                                   t.ID_tipo,
                                   t.Nombre AS NombreTipo,
                                   t.Estado AS EstadoTipo

                               FROM Inmueble i

                               INNER JOIN Propietario p
                                   ON i.ID_propietario = p.ID_propietario

                               INNER JOIN TipoInmueble t
                                   ON i.ID_tipo = t.ID_tipo

                               ORDER BY i.ID_inmueble";

                using (var command = new MySqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var inmueble = new Inmueble
                            {
                                ID_inmueble = reader.GetInt32("ID_inmueble"),
                                Direccion = reader.GetString("Direccion"),
                                Cupo = reader.GetInt32("Cupo"),
                                Coordenadas = reader.GetDecimal("Coordenadas"),
                                PrecioPorDia = reader.GetDecimal("PrecioPorDia"),
                                PorcentajeReserva = reader.GetDecimal("PorcentajeReserva"),
                                Estado = reader.GetBoolean("Estado"),

                                Duenio = new Propietario
                                {
                                    ID_propietario = reader.GetInt32("ID_propietario"),
                                    Nombre = reader.GetString("NombrePropietario"),
                                    Apellido = reader.GetString("ApellidoPropietario"),
                                    DNI = reader.GetString("DNIPropietario"),
                                    Telefono = reader.GetString("TelefonoPropietario"),
                                    Mail = reader.GetString("MailPropietario"),
                                    Estado = reader.GetBoolean("EstadoPropietario")
                                },

                                Tipo = new TipoInmueble
                                {
                                    ID_tipo = reader.GetInt32("ID_tipo"),
                                    Nombre = reader.GetString("NombreTipo"),
                                    Estado = reader.GetBoolean("EstadoTipo")
                                }
                            };

                            lista.Add(inmueble);
                        }
                    }
                }
            }

            return lista;
        }

        public int ObtenerCantidad()
        {
            int cantidad = 0;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM Inmueble";

                using (var command = new MySqlCommand(sql, connection))
                {
                    cantidad = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return cantidad;
        }

        public Inmueble ObtenerPorId(int id)
        {
            Inmueble inmueble = null;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"SELECT
                                   i.ID_inmueble,
                                   i.Direccion,
                                   i.Cupo,
                                   i.Coordenadas,
                                   i.PrecioPorDia,
                                   i.PorcentajeReserva,
                                   i.Estado,

                                   p.ID_propietario,
                                   p.Nombre AS NombrePropietario,
                                   p.Apellido AS ApellidoPropietario,
                                   p.DNI AS DNIPropietario,
                                   p.Telefono AS TelefonoPropietario,
                                   p.Mail AS MailPropietario,
                                   p.Estado AS EstadoPropietario,

                                   t.ID_tipo,
                                   t.Nombre AS NombreTipo,
                                   t.Estado AS EstadoTipo

                               FROM Inmueble i

                               INNER JOIN Propietario p
                                   ON i.ID_propietario = p.ID_propietario

                               INNER JOIN TipoInmueble t
                                   ON i.ID_tipo = t.ID_tipo

                               WHERE i.ID_inmueble = @Id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            inmueble = new Inmueble
                            {
                                ID_inmueble = reader.GetInt32("ID_inmueble"),
                                Direccion = reader.GetString("Direccion"),
                                Cupo = reader.GetInt32("Cupo"),
                                Coordenadas = reader.GetDecimal("Coordenadas"),
                                PrecioPorDia = reader.GetDecimal("PrecioPorDia"),
                                PorcentajeReserva = reader.GetDecimal("PorcentajeReserva"),
                                Estado = reader.GetBoolean("Estado"),

                                Duenio = new Propietario
                                {
                                    ID_propietario = reader.GetInt32("ID_propietario"),
                                    Nombre = reader.GetString("NombrePropietario"),
                                    Apellido = reader.GetString("ApellidoPropietario"),
                                    DNI = reader.GetString("DNIPropietario"),
                                    Telefono = reader.GetString("TelefonoPropietario"),
                                    Mail = reader.GetString("MailPropietario"),
                                    Estado = reader.GetBoolean("EstadoPropietario")
                                },

                                Tipo = new TipoInmueble
                                {
                                    ID_tipo = reader.GetInt32("ID_tipo"),
                                    Nombre = reader.GetString("NombreTipo"),
                                    Estado = reader.GetBoolean("EstadoTipo")
                                }
                            };
                        }
                    }
                }
            }

            return inmueble;
        }
    }
}