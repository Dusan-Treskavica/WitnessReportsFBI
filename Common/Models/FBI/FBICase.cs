using System.Collections.Generic;

namespace Common.Models.FBI
{
    public class FBICase
    {
        public string Url { get; set; }
        public string Uid { get; set; }
        public string Caution { get; set; }
        public string Title { get; set; }
        public string Nationality { get; set; }
        public IList<FBIFile> Files { get; set; }
    }
}
