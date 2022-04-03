using Common.Models.FBI;
using System.Collections.Generic;

namespace Common.Models.CommunicationData
{
    public class FBIApiResponse
    {
        public readonly object Result;

        public int Total { get; set; }
        public IList<FBICase> Items { get; set; }
        public int Page { get; set; }
    }
}
