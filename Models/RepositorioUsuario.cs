using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class RepositorioUsuario : RepositorioBase, IRepositorioUsuario
    {
        public RepositorioUsuario(IConfiguration configuration)
            : base(configuration)
        {
        }

        public int Alta(Usuario usuario)
        {
            using var conexion = ObtenerConexion();

            var sql = @"INSERT INTO Usuario
                        (Nombre, Apellido, Email, Clave, Avatar, Rol, Estado)
                        VALUES
                        (@Nombre, @Apellido, @Email, @Clave, @Avatar, @Rol, @Estado);
                        SELECT LAST_INSERT_ID();";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            comando.Parameters.AddWithValue("@Apellido", usuario.Apellido);
            comando.Parameters.AddWithValue("@Email", usuario.Email);
            comando.Parameters.AddWithValue("@Clave", usuario.Clave);
            comando.Parameters.AddWithValue("@Avatar", usuario.Avatar);
            comando.Parameters.AddWithValue("@Rol", usuario.Rol);
            comando.Parameters.AddWithValue("@Estado", usuario.Estado);

            conexion.Open();

            return Convert.ToInt32(comando.ExecuteScalar());
        }

        public int Baja(int id)
        {
            using var conexion = ObtenerConexion();

            var sql = @"UPDATE Usuario
                        SET Estado = 0
                        WHERE Id = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }

        public int Modificacion(Usuario usuario)
        {
            using var conexion = ObtenerConexion();

            var sql = @"UPDATE Usuario
                        SET Nombre = @Nombre,
                            Apellido = @Apellido,
                            Email = @Email,
                            Clave = @Clave,
                            Avatar = @Avatar,
                            Rol = @Rol
                        WHERE Id = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            comando.Parameters.AddWithValue("@Apellido", usuario.Apellido);
            comando.Parameters.AddWithValue("@Email", usuario.Email);
            comando.Parameters.AddWithValue("@Clave", usuario.Clave);
            comando.Parameters.AddWithValue("@Avatar", usuario.Avatar);
            comando.Parameters.AddWithValue("@Rol", usuario.Rol);
            comando.Parameters.AddWithValue("@Id", usuario.Id);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }

        public IList<Usuario> ObtenerLista()
        {
            var lista = new List<Usuario>();

            using var conexion = ObtenerConexion();

            var sql = @"SELECT Id,
                               Nombre,
                               Apellido,
                               Email,
                               Clave,
                               Avatar,
                               Rol,
                               Estado
                        FROM Usuario";

            using var comando = new MySqlCommand(sql, conexion);

            conexion.Open();

            using var reader = comando.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Usuario
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nombre = reader["Nombre"].ToString() ?? "",
                    Apellido = reader["Apellido"].ToString() ?? "",
                    Email = reader["Email"].ToString() ?? "",
                    Clave = reader["Clave"].ToString() ?? "",
                    Avatar = reader["Avatar"] == DBNull.Value
                        ? null
                        : reader["Avatar"].ToString(),
                    Rol = Convert.ToInt32(reader["Rol"]),
                    Estado = Convert.ToBoolean(reader["Estado"])
                });
            }

            return lista;
        }

        public Usuario? ObtenerPorId(int id)
        {
            using var conexion = ObtenerConexion();

            var sql = @"SELECT Id,
                               Nombre,
                               Apellido,
                               Email,
                               Clave,
                               Avatar,
                               Rol,
                               Estado
                        FROM Usuario
                        WHERE Id = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            using var reader = comando.ExecuteReader();

            if (reader.Read())
            {
                return new Usuario
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nombre = reader["Nombre"].ToString() ?? "",
                    Apellido = reader["Apellido"].ToString() ?? "",
                    Email = reader["Email"].ToString() ?? "",
                    Clave = reader["Clave"].ToString() ?? "",
                    Avatar = reader["Avatar"] == DBNull.Value
                        ? null
                        : reader["Avatar"].ToString(),
                    Rol = Convert.ToInt32(reader["Rol"]),
                    Estado = Convert.ToBoolean(reader["Estado"])
                };
            }

            return null;
        }

        public Usuario? ObtenerPorEmail(string email)
        {
            using var conexion = ObtenerConexion();

            var sql = @"SELECT Id,
                               Nombre,
                               Apellido,
                               Email,
                               Clave,
                               Avatar,
                               Rol,
                               Estado
                        FROM Usuario
                        WHERE Email = @Email";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Email", email);

            conexion.Open();

            using var reader = comando.ExecuteReader();

            if (reader.Read())
            {
                return new Usuario
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nombre = reader["Nombre"].ToString() ?? "",
                    Apellido = reader["Apellido"].ToString() ?? "",
                    Email = reader["Email"].ToString() ?? "",
                    Clave = reader["Clave"].ToString() ?? "",
                    Avatar = reader["Avatar"] == DBNull.Value
                        ? null
                        : reader["Avatar"].ToString(),
                    Rol = Convert.ToInt32(reader["Rol"]),
                    Estado = Convert.ToBoolean(reader["Estado"])
                };
            }

            return null;
        }

        public int AltaEstado(int id)
        {
            using var conexion = ObtenerConexion();

            var sql = @"UPDATE Usuario
                        SET Estado = 1
                        WHERE Id = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }
    }
}