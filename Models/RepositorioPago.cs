using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class RepositorioPago : RepositorioBase, IRepositorioPago
    {
        public RepositorioPago(IConfiguration configuration)
            : base(configuration)
        {
        }

        public int Alta(Pago pago)
        {
            using var conexion = ObtenerConexion();

            var sql = @"INSERT INTO Pago
                        (ID_reserva, Concepto, FechaPago, Monto, Estado)
                        VALUES
                        (@ID_reserva, @Concepto, @FechaPago, @Monto, @Estado);
                        SELECT LAST_INSERT_ID();";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@ID_reserva", pago.ID_reserva);
            comando.Parameters.AddWithValue("@Concepto", pago.Concepto);
            comando.Parameters.AddWithValue("@FechaPago", pago.FechaPago);
            comando.Parameters.AddWithValue("@Monto", pago.Monto);
            comando.Parameters.AddWithValue("@Estado", pago.Estado);

            conexion.Open();

            return Convert.ToInt32(comando.ExecuteScalar());
        }

        public int Baja(int id)
        {
            using var conexion = ObtenerConexion();

            var sql = @"UPDATE Pago
                        SET Estado = 0
                        WHERE ID_pago = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }

        public int Modificacion(Pago pago)
        {
            using var conexion = ObtenerConexion();

            var sql = @"UPDATE Pago
                SET Concepto = @Concepto
                WHERE ID_pago = @ID_pago";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Concepto", pago.Concepto);
            comando.Parameters.AddWithValue("@ID_pago", pago.ID_pago);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }

        public IList<Pago> ObtenerLista()
        {
            var lista = new List<Pago>();

            using var conexion = ObtenerConexion();

            var sql = @"SELECT ID_pago,
                               ID_reserva,
                               Concepto,
                               FechaPago,
                               Monto,
                               Estado
                        FROM Pago";

            using var comando = new MySqlCommand(sql, conexion);

            conexion.Open();

            using var reader = comando.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Pago
                {
                    ID_pago = Convert.ToInt32(reader["ID_pago"]),
                    ID_reserva = Convert.ToInt32(reader["ID_reserva"]),
                    Concepto = Convert.ToString(reader["Concepto"]) ?? "",
                    FechaPago = Convert.ToDateTime(reader["FechaPago"]),
                    Monto = Convert.ToDecimal(reader["Monto"]),
                    Estado = Convert.ToBoolean(reader["Estado"])
                });
            }

            return lista;
        }

        public Pago? ObtenerPorId(int id)
        {
            using var conexion = ObtenerConexion();

            var sql = @"SELECT ID_pago,
                               ID_reserva,
                               Concepto,
                               FechaPago,
                               Monto,
                               Estado
                        FROM Pago
                        WHERE ID_pago = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            using var reader = comando.ExecuteReader();

            if (reader.Read())
            {
                return new Pago
                {
                    ID_pago = Convert.ToInt32(reader["ID_pago"]),
                    ID_reserva = Convert.ToInt32(reader["ID_reserva"]),
                    Concepto = Convert.ToString(reader["Concepto"]) ?? "",
                    FechaPago = Convert.ToDateTime(reader["FechaPago"]),
                    Monto = Convert.ToDecimal(reader["Monto"]),
                    Estado = Convert.ToBoolean(reader["Estado"])
                };
            }

            return null;
        }

        public int AltaEstado(int id)
        {
            using var conexion = ObtenerConexion();

            var sql = @"UPDATE Pago
                        SET Estado = 1
                        WHERE ID_pago = @Id";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }

        public int ObtenerCantidad(string? buscar)
        {
            using var conexion = ObtenerConexion();

            var sql = @"SELECT COUNT(*)
                FROM Pago p
                WHERE
                    @Buscar = ''
                    OR CAST(p.ID_pago AS CHAR) LIKE @Texto
                    OR CAST(p.ID_reserva AS CHAR) LIKE @Texto
                    OR p.Concepto LIKE @Texto";

            using var comando = new MySqlCommand(sql, conexion);

            buscar ??= "";

            comando.Parameters.AddWithValue("@Buscar", buscar);
            comando.Parameters.AddWithValue("@Texto", "%" + buscar + "%");

            conexion.Open();

            return Convert.ToInt32(comando.ExecuteScalar());
        }

        public IList<Pago> ObtenerListaPaginada(
            string? buscar,
            int pagina,
            int cantidadPorPagina)
        {
            var lista = new List<Pago>();

            using var conexion = ObtenerConexion();

            var sql = @"SELECT
                    p.ID_pago,
                    p.ID_reserva,
                    p.Concepto,
                    p.FechaPago,
                    p.Monto,
                    p.Estado
                FROM Pago p
                WHERE
                    @Buscar = ''
                    OR CAST(p.ID_pago AS CHAR) LIKE @Texto
                    OR CAST(p.ID_reserva AS CHAR) LIKE @Texto
                    OR p.Concepto LIKE @Texto
                ORDER BY p.ID_pago
                LIMIT @CantidadPorPagina
                OFFSET @Offset";

            using var comando = new MySqlCommand(sql, conexion);

            buscar ??= "";

            int offset = (pagina - 1) * cantidadPorPagina;

            comando.Parameters.AddWithValue("@Buscar", buscar);
            comando.Parameters.AddWithValue("@Texto", "%" + buscar + "%");
            comando.Parameters.AddWithValue("@CantidadPorPagina", cantidadPorPagina);
            comando.Parameters.AddWithValue("@Offset", offset);

            conexion.Open();

            using var reader = comando.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Pago
                {
                    ID_pago = Convert.ToInt32(reader["ID_pago"]),
                    ID_reserva = Convert.ToInt32(reader["ID_reserva"]),
                    Concepto = Convert.ToString(reader["Concepto"]) ?? "",
                    FechaPago = Convert.ToDateTime(reader["FechaPago"]),
                    Monto = Convert.ToDecimal(reader["Monto"]),
                    Estado = Convert.ToBoolean(reader["Estado"])
                });
            }

            return lista;
        }
    }
}