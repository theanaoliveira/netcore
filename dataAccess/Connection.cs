using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using static dataAccess.EnumConnection;

namespace dataAccess
{
    public class EnumConnection
    {
        public enum StringConnection
        {
            admin = 1,
            SystemAdmin = 2,
            SystemData = 3,
        }
    }

    public class Connection
    {
        public static string GetStringConnection(StringConnection tipoConexao)
        {
            var nameConnection = (int)tipoConexao;
            var lstrConn = ConfigurationManager.ConnectionStrings[nameConnection].ConnectionString;

            return lstrConn;
        }

        /// <summary>
        /// Abre a conexao com o banco de dados
        /// </summary>
        /// <param name="conn">Objeto NpgsqlConnection</param>
        /// <returns>Status da conexao</returns>
        public static ConnectionState OpenConnection(NpgsqlConnection conn)
        {
            conn.Open();
            return conn.State;
        }

        /// <summary>
        /// Fecha a conexão com o bando de dados
        /// </summary>
        /// <param name="conn">Objeto NpgsqlConnection</param>
        /// <returns>Status da conexao</returns>
        public static ConnectionState CloseConnection(NpgsqlConnection conn)
        {
            conn.Close();
            return conn.State;
        }

        public static DataTable ExecuteDataTable<T>(string pstrQuery, T[] parrParameters, T[] parrValueParameters, StringConnection tipoConexao)
        {
            var conn = new NpgsqlConnection(GetStringConnection(tipoConexao));

            OpenConnection(conn);

            var cmd = new NpgsqlCommand(pstrQuery, conn);

            for (var lintCont = 0; lintCont < parrParameters.Length; lintCont++)
                cmd.Parameters.Add(parrParameters[lintCont].ToString(), NpgsqlTypes.NpgsqlDbType.Text).Value = parrValueParameters[lintCont];

            var da = new NpgsqlDataAdapter(cmd);
            var ltblDados = new DataTable();

            da.Fill(ltblDados);

            CloseConnection(conn);

            return ltblDados;
        }
    }
}