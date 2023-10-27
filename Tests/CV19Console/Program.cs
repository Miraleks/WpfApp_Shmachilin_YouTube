using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CV19Console
{
    internal class Program
    {
        const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var responce = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
            return await responce.Content.ReadAsStreamAsync();
        }

        private static async IEnumerable<string> GetDataLines()
        {
            await using var data_stream = await GetDataStream();
            using var data_reader = new StreamReader(data_stream);

            while (!data_reader.EndOfStream)
            {
                var line = await data_reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line)) continue;
            }
        }

        static void Main(string[] args)
        {
            //WebClient client = new WebClient()

            var client = new HttpClient();

            var responce = client.GetAsync(data_url).Result;

            var csv_str = responce.Content.ReadAsStringAsync().Result;

        }
    }
}
