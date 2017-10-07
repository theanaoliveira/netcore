using Npgsql;
using System;
using System.Configuration;
using System.Data;

namespace dataAccess
{
    public class EnumConnection
    {
        public enum StringConnection
        {
            admin = 1,
        }
    }
    
    public class Connection
    {
        public static void GetConnection(string pesquisa, int tipoConexao)
        {
            var nameConnection = tipoConexao;
            var lstrConn = ConfigurationManager.ConnectionStrings[tipoConexao].ConnectionString;

            var conn = new NpgsqlConnection(lstrConn);

            OpenConnection(conn);

            var cmd = new NpgsqlCommand("SELECT name FROM bigdata.facebook WHERE name = @id;", conn);

            cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Text).Value = "itau";

            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Console.WriteLine(dr[0]);
            }
        }

        private static void OpenConnection(NpgsqlConnection conn)
            => conn.Open();

        private static void CloseConnection(NpgsqlConnection conn)
            => conn.Close();
    }
}