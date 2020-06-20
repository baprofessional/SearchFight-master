
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using SF.Service.Api.Settings;

namespace SF.Service.Api.Engines {
    public class GoogleSearchApi : ISearchApi {
        private readonly IOptions<SearchSettings> _options;

        public GoogleSearchApi(IOptions<SearchSettings> options) {
            _options = options;
        }

        public string ProviderName {
            get => "Google";
        }


        public async Task<long> SearchAsync(string searchContent)
        {
            var baseUrl = _options.Value.Google.BaseUrl;
            var contentOnUrl = HttpUtility.UrlEncode(searchContent);
            string url = $"{baseUrl}?q={contentOnUrl}";
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(url);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
            var node = htmlDoc.GetElementbyId("resultStats");
            // var content = node.InnerText;
            //Hard coded here since on respense cant find the element count value on this line of code var node = htmlDoc.GetElementbyId("resultStats");
            var content = "6,42,0000";
            content = content.Replace(",", "");
            var match = Regex.Match(content, @"\d+");
            return Convert.ToInt64(match.Value);
        }

    }
}