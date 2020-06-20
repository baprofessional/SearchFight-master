using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using SF.Service.Api.Settings;

namespace SF.Service.Api.Engines {
    public class BingSearchApi :  ISearchApi {
        private readonly IOptions<SearchSettings> _options;

        public BingSearchApi(IOptions<SearchSettings> options) {
            _options = options;
        }

        public string ProviderName {
            get => "Bing";
        }

        public async Task<long> SearchAsync(string searchContent) {
            var baseUrl = _options.Value.Bing.BaseUrl;
            var contentOnUrl = HttpUtility.UrlEncode(searchContent);
            string url = $"{baseUrl}?q={contentOnUrl}";
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(url);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
            var node = htmlDoc.GetElementbyId("b_tween");
            var content = node.ChildNodes[0].InnerText;
            content = content.Replace(",", "");
            var match = Regex.Match(content, @"\d+");
            return Convert.ToInt64(match.Value);
        }
    }
}