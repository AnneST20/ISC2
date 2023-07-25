using System.Collections.Generic;
using System.Linq;

namespace ISC2
{
    internal class ExcelHelper
    {
        static List<string> urls = new List<string>();

        public static List<string> Read(string path)
        {
            var lines = System.IO.File.ReadAllLines(path).ToList();
            var urls = new List<string>();

            foreach (var line in lines)
            {
                var entries = line.Split(';');
                if (entries.Length > 2 && entries[1].StartsWith("www") && entries[1] != "")
                {
                    urls.Add(entries[1]);
                }
            }

            return urls;
            
        }
    }
}
