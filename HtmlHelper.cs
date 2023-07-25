using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ISC2
{
    internal class HtmlHelper
    {

        private readonly HttpClient _httpClient;

        public HtmlHelper()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent
                .ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
        }
        public async Task<string> GetHtml(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var htmlContent = await response.Content.ReadAsStringAsync();
                return htmlContent;
            }
            catch { return ""; }
        }

        public async Task<List<string>> ParseHtml(string htmlContent)
        {
            if (htmlContent == null || htmlContent.Equals(""))
            {
                return new List<string>();
            }
            var document = new HtmlDocument();
            document.LoadHtml(htmlContent);

            var htmlNodes = document.DocumentNode.Descendants("div")
                .Where(y => y.ParentNode.ParentNode.GetAttributeValue("class", "") == "container")
                .Where(x => x.GetAttributeValue("class", "") == "col-sm-12")
                .ToList();

            var html = new List<string>();

            foreach (var node in htmlNodes)
            {
                if (node != null && !node.Equals(""))
                {
                    html.Add(node.InnerHtml.Trim());
                }
            }

            return html;
        }
    }
}
