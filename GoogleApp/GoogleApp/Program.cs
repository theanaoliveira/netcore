using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace GoogleApp
{



    class Program
    {
        static void Main(string[] args)
        {
            
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

        private static List<NewsModel> GetArticlesNews(string pstrSearch, string pstrKey)
        {
            var lstrUrl = $"https://newsapi.org/v2/everything?q={pstrSearch}&apiKey={pstrKey}&pageSize=100";
            var lListData = new List<NewsModel>();
            var responseFromServer = ReadUrl(lstrUrl);
            var loNews = JsonConvert.DeserializeObject<NewsModel>(responseFromServer);
            var lintPage = 2;
            var lintTotalResults = loNews.totalResults;
            var lbnlCont = true;

            lListData.Add(loNews);

            while (lintPage <= 10 && lbnlCont)
            {
                if (loNews.articles.Count >= lintTotalResults)
                    lbnlCont = false;

                responseFromServer = ReadUrl(lstrUrl + "&page=" + lintPage);
                loNews = JsonConvert.DeserializeObject<NewsModel>(responseFromServer);
                lintPage += 1;

                lListData.Add(loNews);
            }

            return lListData;
        }

        public static List<GoogleModel> GetGoogleSearch(string pstrSearch, string pstrKey)
        {
            var lstrUrl = $"https://www.googleapis.com/customsearch/v1?key={pstrKey}&cx=013036536707430787589:_pqjad5hr1a&q={pstrSearch}&alt=json";
            var lListData = new List<GoogleModel>();
            var responseFromServer = ReadUrl(lstrUrl);
            var loGoogle = JsonConvert.DeserializeObject<GoogleModel>(responseFromServer);
            var lintPage = 11;
            var lintTotalResults = int.Parse(loGoogle.searchInformation.totalResults);
            var lbnlCont = true;

            lListData.Add(loGoogle);

            while (lintPage <= 100 && lbnlCont)
            {
                if (loGoogle.items != null && loGoogle.items.Count >= lintTotalResults)
                    lbnlCont = false;

                responseFromServer = ReadUrl(lstrUrl + "&start=" + lintPage);
                loGoogle = JsonConvert.DeserializeObject<GoogleModel>(responseFromServer);
                lintPage += 10;

                lListData.Add(loGoogle);
            }

            return lListData;
        }

        public static List<NYTModel> GetArticlesNYT(string pstrSearch, string pstrKey)
        {
            var lstrUrl = $"https://api.nytimes.com/svc/search/v2/articlesearch.json?api-key={pstrKey}&q={pstrSearch}&sort=newest";
            var lListData = new List<NYTModel>();
            var responseFromServer = ReadUrl(lstrUrl);
            var loNYT = JsonConvert.DeserializeObject<NYTModel>(responseFromServer);
            var lintPage = 1;

            lListData.Add(loNYT);

            while (lintPage <= 10)
            {
                responseFromServer = ReadUrl(lstrUrl + "&page=" + lintPage);
                loNYT = JsonConvert.DeserializeObject<NYTModel>(responseFromServer);
                lintPage += 1;

                lListData.Add(loNYT);
            }

            return lListData;
        }
    }
}
