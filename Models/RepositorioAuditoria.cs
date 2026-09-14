using MySql.Data.MySqlClient;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public class RepositorioAuditoria : RepositorioBase, IRepositorioAuditoria
    {
        public int Alta(Auditoria auditoria)
        {
            using var conexion = ObtenerConexion();

            var sql = @"INSERT INTO Auditoria
                        (ID_usuario, Entidad, ID_entidad, Accion, Fecha)
                        VALUES
                        (@ID_usuario, @Entidad, @ID_entidad, @Accion, @Fecha)";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@ID_usuario", auditoria.ID_usuario);
            comando.Parameters.AddWithValue("@Entidad", auditoria.Entidad);
            comando.Parameters.AddWithValue("@ID_entidad", auditoria.ID_entidad);
            comando.Parameters.AddWithValue("@Accion", auditoria.Accion);
            comando.Parameters.AddWithValue("@Fecha", auditoria.Fecha);

            conexion.Open();

            return comando.ExecuteNonQuery();
        }

        public IList<Auditoria> ObtenerLista()
        {
            var lista = new List<Auditoria>();

            using var conexion = ObtenerConexion();

            var sql = @"SELECT ID_auditoria,
                               ID_usuario,
                               Entidad,
                               ID_entidad,
                               Accion,
                               Fecha
                        FROM Auditoria
                        ORDER BY Fecha DESC";

            using var comando = new MySqlCommand(sql, conexion);

            conexion.Open();

            using var reader = comando.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Auditoria
                {
                    ID_auditoria = reader.GetInt32("ID_auditoria"),
                    ID_usuario = reader.GetInt32("ID_usuario"),
                    Entidad = reader.GetString("Entidad"),
                    ID_entidad = reader.GetInt32("ID_entidad"),
                    Accion = reader.GetString("Accion"),
                    Fecha = reader.GetDateTime("Fecha")
                });
            }

            return lista;
        }

        public IList<Auditoria> ObtenerPorEntidad(string entidad, int idEntidad)
        {
            var lista = new List<Auditoria>();

            using var conexion = ObtenerConexion();

            var sql = @"SELECT ID_auditoria,
                               ID_usuario,
                               Entidad,
                               ID_entidad,
                               Accion,
                               Fecha
                        FROM Auditoria
                        WHERE Entidad = @Entidad
                          AND ID_entidad = @ID_entidad
                        ORDER BY Fecha DESC";

            using var comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@Entidad", entidad);
            comando.Parameters.AddWithValue("@ID_entidad", idEntidad);

            conexion.Open();

            using var reader = comando.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Auditoria
                {
                    ID_auditoria = reader.GetInt32("ID_auditoria"),
                    ID_usuario = reader.GetInt32("ID_usuario"),
                    Entidad = reader.GetString("Entidad"),
                    ID_entidad = reader.GetInt32("ID_entidad"),
                    Accion = reader.GetString("Accion"),
                    Fecha = reader.GetDateTime("Fecha")
                });
            }

            return lista;
        }
    }
}