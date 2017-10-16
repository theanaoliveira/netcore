using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace dataAccess
{
    public class Connection
    {
        public enum StringConnection
        {
            SystemAdmin = 1,
            SystemData = 2,
            SystemAdminSql = 3,
            SystemDataSql = 4,
        }


        /// <summary>
        /// Recupera a string de conexão
        /// </summary>
        /// <param name="tipoConexao">Enum StringConnection</param>
        /// <returns>a string de conexão da base de dados</returns>
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

        /// <summary>
        /// Executa uma instrução SQL
        /// </summary>
        /// <param name="parrQuery">Uma ou mais querys a serem executadas</param>
        /// <param name="tipoConexao">Enum StringConnection</param>
        public static void ExecuteSql(StringConnection tipoConexao, params string[] parrQuery)
        {
            var conn = new NpgsqlConnection(GetStringConnection(tipoConexao));

            OpenConnection(conn);

            for (var lintCont = 0; lintCont < parrQuery.Length; lintCont++)
            {
                var cmd = new NpgsqlCommand(parrQuery[lintCont], conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            CloseConnection(conn);

            conn.Dispose();
        }

        public static DataTable ExecuteDataTable(string pstrQuery, StringConnection tipoConexao)
        {
            var conn = new NpgsqlConnection(GetStringConnection(tipoConexao));
            var loAdp = new NpgsqlDataAdapter(pstrQuery, conn);
            var ltblDados = new DataTable();

            loAdp.Fill(ltblDados);

            OpenConnection(conn);

            CloseConnection(conn);

            loAdp.Dispose();
            conn.Dispose();

            return ltblDados;
        }
    }
}