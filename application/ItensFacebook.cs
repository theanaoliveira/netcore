using System;
using System.Collections.Generic;
using System.Text;

namespace netcore
{
    public class ItensFacebook
    {
        public class Facebook : IToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
        }

        public class FacebookSearch : ISearch
        {
            public string id { get; set; }
            public string name { get; set; }
            public string about { get; set; }
            public FacebookFeedData feed { get; set; }
        }

        public class FacebookFeed : IFacebookFeed
        {
            public string message { get; set; }
            public string id { get; set; }
        }

        public class FacebookFeedData
        {
            public List<FacebookFeed> data { get; set; }
        }
    }
}
