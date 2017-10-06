using System;
using System.Configuration;
using System.Data.Odbc;

namespace classes
{
    public class EnumConnection
    {
        public enum StringConnection
        {
            teste = 1,
        }
    }


    public class Connection
    {
        public static void GetConnection(string pesquisa)
        {
            var nameConnection = (int)EnumConnection.StringConnection.teste;
            var lstrConn = ConfigurationManager.ConnectionStrings[nameConnection].ConnectionString;
            
            var conn = new OdbcConnection(lstrConn);

            OpenConnection(conn);
            
            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM bigdata.facebook WHERE pesquisa = '@pPesquisa'";
            cmd.Parameters.Add("@pPesquisa", OdbcType.VarChar).Value = "bradesco";

            OdbcDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Console.WriteLine(dr);
            }

        }

        private static void OpenConnection(OdbcConnection conn)
            => conn.Open();

        private static void CloseConnection(OdbcConnection conn)
            => conn.Close();
    }
}
