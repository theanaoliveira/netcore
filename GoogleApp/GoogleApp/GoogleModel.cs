using System.Collections.Generic;

namespace GoogleApp
{
    public class GoogleModel
    {
        public List<GoogleItems> items { get; set; }
        public GoogleSearchInformation searchInformation { get; set; }
    }

    public class GoogleSearchInformation
    {
        public string totalResults { get; set; }
    }

    public class GoogleItems
    {
        public string title { get; set; }
        public string link { get; set; }
        public string snippet { get; set; }
        public GooglePagemap pagemap { get; set; }
    }

    public class GooglePagemap
    {
        public List<GoogleMetatags> metatags { get; set; }
    }

    public class GoogleMetatags
    {
        public string docdatetime { get; set; }
    }
}
