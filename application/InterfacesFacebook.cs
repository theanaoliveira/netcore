using System;
using System.Collections.Generic;
using System.Text;

namespace netcore
{
    interface IToken
    {
        string access_token { get; set; }
        string token_type { get; set; }
    }

    interface ISearch
    {
        string id { get; set; }
        string name { get; set; }
        string about { get; set; }
    }

    interface IFacebookFeed
    {
        string message { get; set; }
        string id { get; set; }
    }
}
