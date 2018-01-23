using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace GoogleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://newsapi.org/docs/endpoints/sources

            //https://webhose.io/auth/signup

            var lstrUrl = "https://newsapi.org/v2/everything?q=BRADESCO&apiKey=614a7a79524e4380bc8470638a3fb76b&pageSize=100";

            var responseFromServer = ReadUrl(lstrUrl);
            var json = JsonConvert.DeserializeObject<NewsModel>(responseFromServer);

            //614a7a79524e4380bc8470638a3fb76b
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
    }
}
