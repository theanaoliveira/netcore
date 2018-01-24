using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleApp
{
    public class NYTModel
    {
        public NYTResponse response { get; set; }
    }


    public class NYTResponse
    {
        public List<NYTDocs> docs { get; set; }
    }

    public class NYTDocs
    {
        public string web_url { get; set; }
        public string snippet { get; set; }
        public string pub_date { get; set; }
    }
}
