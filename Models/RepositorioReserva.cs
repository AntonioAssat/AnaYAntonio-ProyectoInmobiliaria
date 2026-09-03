using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class RepositorioReserva : RepositorioBase, IRepositorioReserva
    {
        public RepositorioReserva(IConfiguration configuration)
            : base(configuration)
        {
        }

        public bool ExisteReservaSuperpuesta(Reserva reserva)
        {
            using var conexion = ObtenerConexion();

            var sql = @"SELECT COUNT(*)
                        FROM Reserva
                        WHERE ID_inmueble = @ID_inmueble
                        AND Estado = 1
                        AND FechaInicio < @FechaFin
                        AND FechaFin > @FechaInicio";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@ID_inmueble", reserva.ID_inmueble);
            comando.Parameters.AddWithValue("@FechaInicio", reserva.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFin", reserva.FechaFin);

            conexion.Open();

            return Convert.ToInt32(comando.ExecuteScalar()) > 0;
        }

        public bool ExisteReservaSuperpuesta(Reserva reserva, int idReservaExcluir)
        {
            using var conexion = ObtenerConexion();

            var sql = @"SELECT COUNT(*)
                        FROM Reserva
                        WHERE ID_inmueble = @ID_inmueble
                        AND Estado = 1
                        AND ID_reserva <> @ID_reserva
                        AND FechaInicio < @FechaFin
                        AND FechaFin > @FechaInicio";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@ID_inmueble", reserva.ID_inmueble);
            comando.Parameters.AddWithValue("@ID_reserva", idReservaExcluir);
            comando.Parameters.AddWithValue("@FechaInicio", reserva.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFin", reserva.FechaFin);

            conexion.Open();

            return Convert.ToInt32(comando.ExecuteScalar()) > 0;
        }
        public int Alta(Reserva reserva)
        {
            using var conexion = ObtenerConexion();

            var sql = @"INSERT INTO Reserva
                        (ID_inquilino, ID_inmueble, FechaInicio, FechaFin, MontoPorDia, Estado)
                        VALUES
                        (@ID_inquilino, @ID_inmueble, @FechaInicio, @FechaFin, @MontoPorDia, @Estado);
                        SELECT LAST_INSERT_ID();";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@ID_inquilino", reserva.ID_inquilino);
            comando.Parameters.AddWithValue("@ID_inmueble", reserva.ID_inmueble);
            comando.Parameters.AddWithValue("@FechaInicio", reserva.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFin", reserva.FechaFin);
            comando.Parameters.AddWithValue("@MontoPorDia", reserva.MontoPorDia);
            comando.Parameters.AddWithValue("@Estado", reserva.Estado);

            conexion.Open();

            return Convert.ToInt32(comando.ExecuteScalar());
        }

        public int Baja(int id)
        {
            using var conexion = ObtenerConexion();

            var sql = @"UPDATE Reserva
                        SET Estado = 0
                        WHERE ID_reserva = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }

        public int Modificacion(Reserva reserva)
        {
            using var conexion = ObtenerConexion();

            var sql = @"UPDATE Reserva
                        SET ID_inquilino = @ID_inquilino,
                            ID_inmueble = @ID_inmueble,
                            FechaInicio = @FechaInicio,
                            FechaFin = @FechaFin,
                            MontoPorDia = @MontoPorDia
                        WHERE ID_reserva = @ID_reserva";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@ID_inquilino", reserva.ID_inquilino);
            comando.Parameters.AddWithValue("@ID_inmueble", reserva.ID_inmueble);
            comando.Parameters.AddWithValue("@FechaInicio", reserva.FechaInicio);
            comando.Parameters.AddWithValue("@FechaFin", reserva.FechaFin);
            comando.Parameters.AddWithValue("@MontoPorDia", reserva.MontoPorDia);
            comando.Parameters.AddWithValue("@ID_reserva", reserva.ID_reserva);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }

        public IList<Reserva> ObtenerLista()
        {
            var lista = new List<Reserva>();

            using var conexion = ObtenerConexion();

            var sql = @"SELECT ID_reserva,
                               ID_inquilino,
                               ID_inmueble,
                               FechaInicio,
                               FechaFin,
                               MontoPorDia,
                               Estado
                        FROM Reserva";

            using var comando = new MySqlCommand(sql, conexion);

            conexion.Open();

            using var reader = comando.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Reserva
                {
                    ID_reserva = Convert.ToInt32(reader["ID_reserva"]),
                    ID_inquilino = Convert.ToInt32(reader["ID_inquilino"]),
                    ID_inmueble = Convert.ToInt32(reader["ID_inmueble"]),
                    FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                    FechaFin = Convert.ToDateTime(reader["FechaFin"]),
                    MontoPorDia = Convert.ToDecimal(reader["MontoPorDia"]),
                    Estado = Convert.ToBoolean(reader["Estado"])
                });
            }

            return lista;
        }

        public Reserva? ObtenerPorId(int id)
        {
            using var conexion = ObtenerConexion();

            var sql = @"SELECT ID_reserva,
                               ID_inquilino,
                               ID_inmueble,
                               FechaInicio,
                               FechaFin,
                               MontoPorDia,
                               Estado
                        FROM Reserva
                        WHERE ID_reserva = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            using var reader = comando.ExecuteReader();

            if (reader.Read())
            {
                return new Reserva
                {
                    ID_reserva = Convert.ToInt32(reader["ID_reserva"]),
                    ID_inquilino = Convert.ToInt32(reader["ID_inquilino"]),
                    ID_inmueble = Convert.ToInt32(reader["ID_inmueble"]),
                    FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                    FechaFin = Convert.ToDateTime(reader["FechaFin"]),
                    MontoPorDia = Convert.ToDecimal(reader["MontoPorDia"]),
                    Estado = Convert.ToBoolean(reader["Estado"])
                };
            }

            return null;
        }

        public int AltaEstado(int id)
        {
            using var conexion = ObtenerConexion();

            var sql = @"UPDATE Reserva
                        SET Estado = 1
                        WHERE ID_reserva = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }
    }
}