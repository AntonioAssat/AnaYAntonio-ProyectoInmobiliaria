using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class RepositorioInquilino : RepositorioBase, IRepositorioInquilino
    {
        public RepositorioInquilino(IConfiguration configuration)
            : base(configuration)
        {
        }

        // ALTA
        public int Alta(Inquilino p)
        {
            using var conexion = ObtenerConexion();

            var sql = @"INSERT INTO Inquilino
                        (NombreCompleto, DNI, Telefono, Mail, Estado)
                        VALUES
                        (@NombreCompleto, @DNI, @Telefono, @Mail, @Estado);
                        SELECT LAST_INSERT_ID();";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@NombreCompleto", p.NombreCompleto);
            comando.Parameters.AddWithValue("@DNI", p.DNI);
            comando.Parameters.AddWithValue("@Telefono", p.Telefono);
            comando.Parameters.AddWithValue("@Mail", p.Mail);
            comando.Parameters.AddWithValue("@Estado", p.Estado);

            conexion.Open();

            return Convert.ToInt32(comando.ExecuteScalar());
        }

        // BAJA
        public int Baja(int id)
        {
            using var conexion = ObtenerConexion();

            var sql = @"DELETE FROM Inquilino
                        WHERE ID_inquilino = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }

        // MODIFICACIÓN
        public int Modificacion(Inquilino p)
        {
            using var conexion = ObtenerConexion();

            var sql = @"UPDATE Inquilino
                        SET NombreCompleto = @NombreCompleto,
                            DNI = @DNI,
                            Telefono = @Telefono,
                            Mail = @Mail,
                            Estado = @Estado
                        WHERE ID_inquilino = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@NombreCompleto", p.NombreCompleto);
            comando.Parameters.AddWithValue("@DNI", p.DNI);
            comando.Parameters.AddWithValue("@Telefono", p.Telefono);
            comando.Parameters.AddWithValue("@Mail", p.Mail);
            comando.Parameters.AddWithValue("@Estado", p.Estado);
            comando.Parameters.AddWithValue("@Id", p.ID_inquilino);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }

        // OBTENER LISTA
        public IList<Inquilino> ObtenerLista()
        {
            var lista = new List<Inquilino>();

            using var conexion = ObtenerConexion();

            var sql = @"SELECT ID_inquilino, NombreCompleto, DNI,
                               Telefono, Mail, Estado
                        FROM Inquilino";

            using var comando = new MySqlCommand(sql, conexion);

            conexion.Open();

            using var reader = comando.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Inquilino
                {
                    ID_inquilino = Convert.ToInt32(reader["ID_inquilino"]),
                    NombreCompleto = reader["NombreCompleto"].ToString()!,
                    DNI = reader["DNI"].ToString()!,
                    Telefono = reader["Telefono"].ToString()!,
                    Mail = reader["Mail"].ToString()!,
                    Estado = Convert.ToBoolean(reader["Estado"])
                });
            }

            return lista;
        }

        // OBTENER POR ID
        public Inquilino? ObtenerPorId(int id)
        {
            using var conexion = ObtenerConexion();

            var sql = @"SELECT ID_inquilino, NombreCompleto, DNI,
                               Telefono, Mail, Estado
                        FROM Inquilino
                        WHERE ID_inquilino = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            using var reader = comando.ExecuteReader();

            if (reader.Read())
            {
                return new Inquilino
                {
                    ID_inquilino = Convert.ToInt32(reader["ID_inquilino"]),
                    NombreCompleto = reader["NombreCompleto"].ToString()!,
                    DNI = reader["DNI"].ToString()!,
                    Telefono = reader["Telefono"].ToString()!,
                    Mail = reader["Mail"].ToString()!,
                    Estado = Convert.ToBoolean(reader["Estado"])
                };
            }

            return null;
        }
        // CAMBIAR ESTADO
        public int AltaEstado(int id)
        {
            using var conexion = ObtenerConexion();

            var sql = @"UPDATE Inquilino
                        SET Estado = NOT Estado
                        WHERE ID_inquilino = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }
    }
}