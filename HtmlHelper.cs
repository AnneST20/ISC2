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
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var htmlContent = await response.Content.ReadAsStringAsync();
            return htmlContent;
        }

        public async Task<List<string>> GetHtmls(List<string> urls)
        {
            var htmlContents = new List<string>();
            var tasks = urls.Select(async url =>
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var html = await response.Content.ReadAsStringAsync();
                        htmlContents.Add(html);
                    }
                    else
                    {
                        // handle the error response here
                    }
                }
            });

            await Task.WhenAll(tasks);
            return htmlContents;
        }

        public async Task<string> ParseHtml(string htmlContent)
        {
            var document = new HtmlDocument();
            document.LoadHtml(htmlContent);

            var html = document.DocumentNode.Descendants("div")
                .Where(x => x.ParentNode.GetAttributeValue("class", "") == "col-sm-12")
                .FirstOrDefault()?.InnerHtml.Trim();

            return html;
        }

        public async Task<List<string>> PerseHtmls(List<string> htmlContents)
        {
            var htmls = new List<string>();

            var tasks = htmlContents.Select(async htmlContent =>
            {
                htmls.Add(await ParseHtml(htmlContent));
            });

            await Task.WhenAll(tasks);
            return htmls;
        }
    }
}
