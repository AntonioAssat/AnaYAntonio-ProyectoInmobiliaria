using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class RepositorioInformes : RepositorioBase, IRepositorioInformes
    {
        public RepositorioInformes(IConfiguration configuration)
            : base(configuration)
        {
        }

        public IList<Inmueble> ObtenerInmueblesPorPropietario(int idPropietario)
        {
            var lista = new List<Inmueble>();

            using var connection = ObtenerConexion();
            connection.Open();

            using var cmd = new MySqlCommand(@"
                SELECT
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

                    t.ID_tipo,
                    t.Nombre AS NombreTipo

                FROM inmueble i
                INNER JOIN propietario p
                    ON i.ID_propietario = p.ID_propietario
                INNER JOIN tipoinmueble t
                    ON i.ID_tipo = t.ID_tipo

                WHERE i.ID_propietario = @IdPropietario

                ORDER BY i.ID_inmueble
            ", connection);

            cmd.Parameters.AddWithValue("@IdPropietario", idPropietario);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Inmueble
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
                        Apellido = reader.GetString("ApellidoPropietario")
                    },

                    Tipo = new TipoInmueble
                    {
                        ID_tipo = reader.GetInt32("ID_tipo"),
                        Nombre = reader.GetString("NombreTipo")
                    }
                });
            }

            return lista;
        }


        public IList<Inmueble> ObtenerInmueblesMasReservados(DateTime desde)
        {
            var lista = new List<Inmueble>();

            using var connection = ObtenerConexion();
            connection.Open();

            using var cmd = new MySqlCommand(@"
                SELECT
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

                    t.ID_tipo,
                    t.Nombre AS NombreTipo,

                    COUNT(r.ID_reserva) AS CantidadReservas

                FROM inmueble i
                INNER JOIN reserva r
                    ON i.ID_inmueble = r.ID_inmueble
                INNER JOIN propietario p
                    ON i.ID_propietario = p.ID_propietario
                INNER JOIN tipoinmueble t
                    ON i.ID_tipo = t.ID_tipo

                WHERE r.FechaInicio >= @Desde

                GROUP BY
                    i.ID_inmueble,
                    i.Direccion,
                    i.Cupo,
                    i.Coordenadas,
                    i.PrecioPorDia,
                    i.PorcentajeReserva,
                    i.Estado,
                    p.ID_propietario,
                    p.Nombre,
                    p.Apellido,
                    t.ID_tipo,
                    t.Nombre

                ORDER BY CantidadReservas DESC, i.ID_inmueble
            ", connection);

            cmd.Parameters.AddWithValue("@Desde", desde);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Inmueble
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
                        Apellido = reader.GetString("ApellidoPropietario")
                    },

                    Tipo = new TipoInmueble
                    {
                        ID_tipo = reader.GetInt32("ID_tipo"),
                        Nombre = reader.GetString("NombreTipo")
                    }
                });
            }

            return lista;
        }


        public IList<Inmueble> ObtenerInmueblesSinReservas(DateTime desde)
        {
            var lista = new List<Inmueble>();

            using var connection = ObtenerConexion();
            connection.Open();

            using var cmd = new MySqlCommand(@"
                SELECT
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

                    t.ID_tipo,
                    t.Nombre AS NombreTipo

                FROM inmueble i

                INNER JOIN propietario p
                    ON i.ID_propietario = p.ID_propietario

                INNER JOIN tipoinmueble t
                    ON i.ID_tipo = t.ID_tipo

                WHERE NOT EXISTS (
                    SELECT 1
                    FROM reserva r
                    WHERE r.ID_inmueble = i.ID_inmueble
                      AND r.FechaInicio >= @Desde
                )

                ORDER BY i.ID_inmueble
            ", connection);

            cmd.Parameters.AddWithValue("@Desde", desde);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Inmueble
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
                        Apellido = reader.GetString("ApellidoPropietario")
                    },

                    Tipo = new TipoInmueble
                    {
                        ID_tipo = reader.GetInt32("ID_tipo"),
                        Nombre = reader.GetString("NombreTipo")
                    }
                });
            }

            return lista;
        }


        public IList<Reserva> ObtenerReservasVigentes(
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            var lista = new List<Reserva>();

            using var connection = ObtenerConexion();
            connection.Open();

            using var cmd = new MySqlCommand(@"
                SELECT
                    r.ID_reserva,
                    r.ID_inquilino,
                    r.ID_inmueble,
                    r.FechaInicio,
                    r.FechaFin,
                    r.FechaFinEfectiva,
                    r.MontoPorDia,
                    r.Estado

                FROM reserva r

                WHERE r.Estado = 1
                  AND r.FechaInicio <= @FechaFin
                  AND r.FechaFin >= @FechaInicio

                ORDER BY r.FechaInicio
            ", connection);

            cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
            cmd.Parameters.AddWithValue("@FechaFin", fechaFin);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Reserva
                {
                    ID_reserva = reader.GetInt32("ID_reserva"),
                    ID_inquilino = reader.GetInt32("ID_inquilino"),
                    ID_inmueble = reader.GetInt32("ID_inmueble"),
                    FechaInicio = reader.GetDateTime("FechaInicio"),
                    FechaFin = reader.GetDateTime("FechaFin"),
                    FechaFinEfectiva = reader.IsDBNull(reader.GetOrdinal("FechaFinEfectiva"))
                        ? null
                        : reader.GetDateTime("FechaFinEfectiva"),
                    MontoPorDia = reader.GetDecimal("MontoPorDia"),
                    Estado = reader.GetBoolean("Estado")
                });
            }

            return lista;
        }


        public IList<Reserva> ObtenerReservasQueTerminan(DateTime hasta)
        {
            var lista = new List<Reserva>();

            using var connection = ObtenerConexion();
            connection.Open();

            using var cmd = new MySqlCommand(@"
                SELECT
                    r.ID_reserva,
                    r.ID_inquilino,
                    r.ID_inmueble,
                    r.FechaInicio,
                    r.FechaFin,
                    r.FechaFinEfectiva,
                    r.MontoPorDia,
                    r.Estado

                FROM reserva r

                WHERE r.Estado = 1
                  AND r.FechaFin >= CURDATE()
                  AND r.FechaFin <= @Hasta

                ORDER BY r.FechaFin
            ", connection);

            cmd.Parameters.AddWithValue("@Hasta", hasta);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Reserva
                {
                    ID_reserva = reader.GetInt32("ID_reserva"),
                    ID_inquilino = reader.GetInt32("ID_inquilino"),
                    ID_inmueble = reader.GetInt32("ID_inmueble"),
                    FechaInicio = reader.GetDateTime("FechaInicio"),
                    FechaFin = reader.GetDateTime("FechaFin"),
                    FechaFinEfectiva = reader.IsDBNull(reader.GetOrdinal("FechaFinEfectiva"))
                        ? null
                        : reader.GetDateTime("FechaFinEfectiva"),
                    MontoPorDia = reader.GetDecimal("MontoPorDia"),
                    Estado = reader.GetBoolean("Estado")
                });
            }

            return lista;
        }


        public IList<Pago> ObtenerPagosPorReserva(int idReserva)
        {
            var lista = new List<Pago>();

            using var connection = ObtenerConexion();
            connection.Open();

            using var cmd = new MySqlCommand(@"
                SELECT
                    ID_pago,
                    ID_reserva,
                    Concepto,
                    FechaPago,
                    Monto,
                    Estado

                FROM pago

                WHERE ID_reserva = @IdReserva

                ORDER BY FechaPago DESC
            ", connection);

            cmd.Parameters.AddWithValue("@IdReserva", idReserva);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Pago
                {
                    ID_pago = reader.GetInt32("ID_pago"),
                    ID_reserva = reader.GetInt32("ID_reserva"),
                    Concepto = reader.IsDBNull(reader.GetOrdinal("Concepto"))
                        ? ""
                        : reader.GetString("Concepto"),
                    FechaPago = reader.GetDateTime("FechaPago"),
                    Monto = reader.GetDecimal("Monto"),
                    Estado = reader.GetBoolean("Estado")
                });
            }

            return lista;
        }


        public IList<Inmueble> ObtenerInmueblesDisponibles(
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            var lista = new List<Inmueble>();

            using var connection = ObtenerConexion();
            connection.Open();

            using var cmd = new MySqlCommand(@"
                SELECT
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

                    t.ID_tipo,
                    t.Nombre AS NombreTipo

                FROM inmueble i

                INNER JOIN propietario p
                    ON i.ID_propietario = p.ID_propietario

                INNER JOIN tipoinmueble t
                    ON i.ID_tipo = t.ID_tipo

                WHERE i.Estado = 1

                  AND NOT EXISTS (
                      SELECT 1
                      FROM reserva r
                      WHERE r.ID_inmueble = i.ID_inmueble
                        AND r.Estado = 1
                        AND r.FechaInicio < @FechaFin
                        AND r.FechaFin > @FechaInicio
                  )

                ORDER BY i.ID_inmueble
            ", connection);

            cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
            cmd.Parameters.AddWithValue("@FechaFin", fechaFin);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Inmueble
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
                        Apellido = reader.GetString("ApellidoPropietario")
                    },

                    Tipo = new TipoInmueble
                    {
                        ID_tipo = reader.GetInt32("ID_tipo"),
                        Nombre = reader.GetString("NombreTipo")
                    }
                });
            }

            return lista;
        }

        public IList<Reserva> BuscarReservas(string texto)
        {
            var lista = new List<Reserva>();

            using var connection = ObtenerConexion();
            connection.Open();

            using var cmd = new MySqlCommand(@"
                SELECT
                    r.ID_reserva,
                    r.ID_inquilino,
                    r.ID_inmueble,
                    r.FechaInicio,
                    r.FechaFin,
                    r.FechaFinEfectiva,
                    r.MontoPorDia,
                    r.Estado

                FROM reserva r

                WHERE CAST(r.ID_reserva AS CHAR) LIKE @Texto
                   OR CAST(r.ID_inquilino AS CHAR) LIKE @Texto
                   OR CAST(r.ID_inmueble AS CHAR) LIKE @Texto

                ORDER BY r.ID_reserva DESC
                LIMIT 20
            ", connection);

            cmd.Parameters.AddWithValue("@Texto", "%" + texto + "%");

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Reserva
                {
                    ID_reserva = reader.GetInt32("ID_reserva"),
                    ID_inquilino = reader.GetInt32("ID_inquilino"),
                    ID_inmueble = reader.GetInt32("ID_inmueble"),
                    FechaInicio = reader.GetDateTime("FechaInicio"),
                    FechaFin = reader.GetDateTime("FechaFin"),
                    FechaFinEfectiva = reader.IsDBNull(
                        reader.GetOrdinal("FechaFinEfectiva"))
                        ? null
                        : reader.GetDateTime("FechaFinEfectiva"),
                    MontoPorDia = reader.GetDecimal("MontoPorDia"),
                    Estado = reader.GetBoolean("Estado")
                });
            }

            return lista;
        }
    }
}