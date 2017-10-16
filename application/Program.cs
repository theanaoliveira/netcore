using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using static netcore.ItensFacebook;
using dataAccess;

namespace netcore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            GetInfoFacebook();
        }

        /// <summary>
        /// Le uma determinada URL e devolve o retorno
        /// </summary>
        /// <param name="pstrUrl">Url a ser lida</param>
        /// <returns>string com o retorno da url</returns>
        static string ReadUrl(string pstrUrl)
        {
            var url = pstrUrl;
            var req = (HttpWebRequest)WebRequest.Create(url);
            var res = req.GetResponse();

            // Get the stream containing content returned by the server.
            var dataStream = res.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            var reader = new StreamReader(dataStream);

            // Read the content.
            var responseFromServer = reader.ReadToEnd();

            reader.Close();
            res.Close();

            return responseFromServer;
        }

        /// <summary>
        /// Recupera o token do facebook
        /// </summary>
        /// <returns>objeto Facebook com o token</returns>
        static Facebook GetToken()
        {
            var larrParameters = new string[1] { "@api" };
            var larrValues = new string[1] { "facebook" };
            var ltblDadosKey = "";// Connection.ExecuteDataTable<string>("select client_id, client_secret from bigdata.api_key where api = @api", larrParameters, larrValues, EnumConnection.StringConnection.admin);
            var lstrClientId = "";
            var lstrClientSecret = "";

            if (ltblDadosKey.Rows.Count > 0)
            {
                lstrClientId = ltblDadosKey.Rows[0]["client_id"].ToString();
                lstrClientSecret = ltblDadosKey.Rows[0]["client_secret"].ToString();
            }

            var UrlToken = $"https://graph.facebook.com/oauth/access_token?%20client_id={lstrClientId}&client_secret={lstrClientSecret}&grant_type=client_credentials";
            var responseFromServer = ReadUrl(UrlToken);
            var json = JsonConvert.DeserializeObject<Facebook>(responseFromServer);

            return json;
        }

        /// <summary>
        /// Busca uma pagina no facebook
        /// </summary>
        /// <param name="pstrAccessToken">Token de acesso</param>
        /// <param name="pstrName">Nome da pagina</param>
        /// <returns>Objeto com o retorno da pagina</returns>
        static FacebookSearch SearchPage(string pstrAccessToken, string pstrName)
        {
            var UrlSearch = "https://graph.facebook.com/v2.10/" + pstrName + "?fields=id,name,about,feed{message}&since=1504224000&until=1506816000&access_token=" + pstrAccessToken;
            var responseFromServer = ReadUrl(UrlSearch);
            var json = JsonConvert.DeserializeObject<FacebookSearch>(responseFromServer);

            return json;
        }

        /// <summary>
        /// Inicia a chamada ao facebook
        /// </summary>
        static void GetInfoFacebook()
        {
            Console.WriteLine("\nDigite o nome da pagina que deseja buscar?");
            var name = Console.ReadLine();

            while (String.IsNullOrEmpty(name))
            {
                Console.WriteLine("\nDigite o nome da pagina que deseja buscar?");
                name = Console.ReadLine();
            }

            var loToken = GetToken();

            try
            {
                var loFacebook = SearchPage(loToken.access_token, name);

                Console.WriteLine($"\n\nTermo pesquisado: {name}\n\nPágina encontrada: {loFacebook.name}, {loFacebook.about}");

                Console.WriteLine("\n\nPostagens:");

                for (var i = 0; i < loFacebook.feed.data.Count; i++)
                    Console.WriteLine($"\n{loFacebook.feed.data[i].message}");

                ContinueSearch();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Termo não encontrado: {name} : {e.Message}");
                ContinueSearch();
            }
        }

        /// <summary>
        /// Pergunta ao usuário se deve continuar a busca
        /// </summary>
        static void ContinueSearch()
        {
            Console.WriteLine("\n\nDeseja buscar outro item?\n1 - Sim\n2 - Não");

            var item = Console.ReadLine();

            if (item == "1")
                GetInfoFacebook();
            else
            {
                Console.WriteLine("\nPress any key to exit");
                Console.ReadKey(true);
            }
        }
    }
}