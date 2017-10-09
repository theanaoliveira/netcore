using dataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;

namespace application.test
{
    [TestClass]
    public class UnitTest1
    {
        #region DataAccess

        [TestMethod]
        public void TestaStringConexaoVazia()
        {
            var lstrConn = Connection.GetStringConnection(EnumConnection.StringConnection.admin);

            Assert.IsNotNull(lstrConn);
        }

        [TestMethod]
        public void TestaAbrirConexaoComBanco()
        {
            var conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=test_application;Uid=postgres;Pwd=exec@2017;");

            var status = Connection.OpenConnection(conn);

            Assert.AreEqual("Open", status.ToString());
        }

        [TestMethod]
        public void TestaFecharConexaoComBanco()
        {
            var conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=test_application;Uid=postgres;Pwd=exec@2017;");

            var status = Connection.CloseConnection(conn);

            Assert.AreEqual("Closed", status.ToString());
        }

        #endregion
    }
}
