using System.Collections.Generic;

namespace SF.Service.Data {
    public class SearchFightResult {
        public Dictionary<string,IEnumerable<SearcherApiResult>> ApiResults { get; set; }
        public Dictionary<string,string> WinnerByEngine { get; set; }
        public string WinnerTerm { get; set; }
    }
}