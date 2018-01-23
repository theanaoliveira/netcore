using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleApp
{
    public class NewsModel
    {
        public string status { get; set; }
        public int totalResults { get; set; }
        public List<NewsArticle> articles { get; set; }
    }

    public class NewsArticle
    {
        public NewsSource source { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string urlToImage { get; set; }
        public string publishedAt { get; set; }
    }

    public class NewsSource
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
