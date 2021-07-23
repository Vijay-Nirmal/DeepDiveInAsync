using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncBasicWithReturn
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var content = await GetWebpageAsync();

            await File.WriteAllTextAsync("D:/Site/content.html", content);
        }

        public static async Task<string> GetWebpageAsync()
        {
            var client = new HttpClient();
            var content = await client.GetStringAsync("https://dotnet.microsoft.com/");
            return content;
        }
    }
}
