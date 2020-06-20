using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SF.Service.Api;

namespace SF.Service.Data {
    public class SearcherFightService : ISearcherFightService {
        private readonly IEnumerable<ISearchApi> _apis;

        public SearcherFightService(IEnumerable<ISearchApi> apis) {
            _apis = apis;
        }
        public async Task<SearchFightResult> FightAsync(string[] args) {
            if(args == null)
                throw new Exception("No searches to be done");
            if(args.Length <=1)
                throw new Exception("To start the fight, the minimun terms required is 2");
            var apiResults = new List<SearcherApiResult>();
            foreach (ISearchApi api in _apis) {
                foreach (string term in args) {
                    apiResults.Add(new SearcherApiResult() {
                        Engine = api.ProviderName,
                        Term = term,
                        ResultCount = await api.SearchAsync(term)
                    });
                }
            }
            SearchFightResult result = new SearchFightResult();
            //Calculating winner by engine
            result.ApiResults = apiResults.GroupBy(r => r.Term).ToDictionary(f => f.Key, v => v.AsEnumerable());
            result.WinnerByEngine = apiResults.GroupBy(r => r.Engine).ToDictionary(k => k.Key,v => v.AsEnumerable().Where(x=>x.Engine == v.Key).Aggregate((item1,item2)=>item1.ResultCount<item2.ResultCount?item2:item1).Term);
            result.WinnerTerm = apiResults.Aggregate((item1,item2)=>item1.ResultCount<item2.ResultCount?item2:item1).Term;
            return result;
        }
    }
}