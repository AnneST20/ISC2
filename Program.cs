using System;
using System.IO;
using System.Reflection.Metadata;

namespace ISC2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var html = new HtmlHelper();
            string output = "C:\\Users\\anna.stetsenko\\source\\repos\\ISC2\\output.csv";
            var urls = ExcelHelper.Read(output);

            for (int u = 0; u < urls.Count; u++)
            {
                var fileName = "C:\\Users\\anna.stetsenko\\source\\repos\\ISC2\\Results\\" + urls[u].Replace("www.isc2.org/", "").Replace("/", "-") + ".txt";
                urls[u] = "https://" + urls[u];

                var htmlContent = html.GetHtml(urls[u]).Result;
                var htmlParsedContent = html.ParseHtml(htmlContent).Result;

                File.WriteAllText(fileName, htmlParsedContent);
            }
        }
    }
}
