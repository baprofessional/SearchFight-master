using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SF.Service.Data;

namespace Searchfight {
    public class AppResult {
        private readonly ISearcherFightService _searchFightService;

        public AppResult(ISearcherFightService searchFightService) {
            _searchFightService = searchFightService;
        }
        private const string SEPARATOR =" ";
        private const string DEFINE = ":";
        private const string WINNER = "winner";
        private const string TOTAL = "Total";

        public async Task RunAsync(string[] args) {
            try {
                var result = await _searchFightService.FightAsync(args);
                foreach (KeyValuePair<string, IEnumerable<SearcherApiResult>> resultApiResult in result.ApiResults) {
                    StringBuilder sBuilder = new StringBuilder();
                    sBuilder.Append(resultApiResult.Key);
                    sBuilder.Append($"{DEFINE}{SEPARATOR}");
                    foreach (SearcherApiResult apiResult in resultApiResult.Value) {
                        sBuilder.Append($"{apiResult.Engine}{DEFINE}{SEPARATOR}{apiResult.ResultCount}{SEPARATOR}");
                    }
                    Console.WriteLine(sBuilder.ToString().Trim());
                }

                foreach (KeyValuePair<string, string> pair in result.WinnerByEngine) {
                    Console.WriteLine($"{pair.Key}{SEPARATOR}{WINNER}{DEFINE}{SEPARATOR}{pair.Value}");
                }
                Console.WriteLine($"{TOTAL}{SEPARATOR}{WINNER}{DEFINE}{SEPARATOR}{result.WinnerTerm}");
            }
            catch (Exception exc) {
                Console.WriteLine(exc.Message);
            }
        }
    }
}
