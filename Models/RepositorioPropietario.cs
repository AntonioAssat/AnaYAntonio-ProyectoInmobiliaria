using System;
using System.Collections.Generic;
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
            using var conexion = ObtenerConexion();

            var sql = @"INSERT INTO Propietario
                        (Nombre, Apellido, DNI, Telefono, Mail, Estado)
                        VALUES
                        (@Nombre, @Apellido, @DNI, @Telefono, @Mail, @Estado);
                        SELECT LAST_INSERT_ID();";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Nombre", p.Nombre);
            comando.Parameters.AddWithValue("@Apellido", p.Apellido);
            comando.Parameters.AddWithValue("@DNI", p.DNI);
            comando.Parameters.AddWithValue("@Telefono", p.Telefono);
            comando.Parameters.AddWithValue("@Mail", p.Mail);
            comando.Parameters.AddWithValue("@Estado", p.Estado);

            conexion.Open();

            return Convert.ToInt32(comando.ExecuteScalar());
        }


        public int Baja(int id)
        {
            using var conexion = ObtenerConexion();

            var sql = @"UPDATE Propietario
                        SET Estado = false
                        WHERE ID_propietario = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }


        public int Modificacion(Propietario p)
        {
            using var conexion = ObtenerConexion();

            var sql = @"UPDATE Propietario
                        SET Nombre = @Nombre,
                            Apellido = @Apellido,
                            DNI = @DNI,
                            Telefono = @Telefono,
                            Mail = @Mail
                        WHERE ID_propietario = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Nombre", p.Nombre);
            comando.Parameters.AddWithValue("@Apellido", p.Apellido);
            comando.Parameters.AddWithValue("@DNI", p.DNI);
            comando.Parameters.AddWithValue("@Telefono", p.Telefono);
            comando.Parameters.AddWithValue("@Mail", p.Mail);
            comando.Parameters.AddWithValue("@Id", p.ID_propietario);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }

        public IList<Propietario> ObtenerLista()
        {
            var lista = new List<Propietario>();

            using var conexion = ObtenerConexion();

            var sql = @"SELECT ID_propietario, Nombre, Apellido, DNI,
                               Telefono, Mail, Estado
                        FROM Propietario";

            using var comando = new MySqlCommand(sql, conexion);

            conexion.Open();

            using var reader = comando.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Propietario
                {
                    ID_propietario = Convert.ToInt32(reader["ID_propietario"]),
                    Nombre = reader["Nombre"].ToString()!,
                    Apellido = reader["Apellido"].ToString()!,
                    DNI = reader["DNI"].ToString()!,
                    Telefono = reader["Telefono"].ToString()!,
                    Mail = reader["Mail"].ToString()!,
                    Estado = Convert.ToBoolean(reader["Estado"])
                });
            }

            return lista;
        }

        public Propietario? ObtenerPorId(int id)
        {
            using var conexion = ObtenerConexion();

            var sql = @"SELECT ID_propietario, Nombre, Apellido, DNI,
                               Telefono, Mail, Estado
                        FROM Propietario
                        WHERE ID_propietario = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            using var reader = comando.ExecuteReader();

            if (reader.Read())
            {
                return new Propietario
                {
                    ID_propietario = Convert.ToInt32(reader["ID_propietario"]),
                    Nombre = reader["Nombre"].ToString()!,
                    Apellido = reader["Apellido"].ToString()!,
                    DNI = reader["DNI"].ToString()!,
                    Telefono = reader["Telefono"].ToString()!,
                    Mail = reader["Mail"].ToString()!,
                    Estado = Convert.ToBoolean(reader["Estado"])
                };
            }

            return null;
        }

        public int AltaEstado(int id)
        {
            using var conexion = ObtenerConexion();

            var sql = @"UPDATE Propietario
                        SET Estado = true
                        WHERE ID_propietario = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }
    }
}