using System.Globalization;

namespace CV19Console
{
    internal class Program
    {
        const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient(); // создаем подключение 
            var responce = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead); // получаем с сервера только заголовки ответа
            return await responce.Content.ReadAsStreamAsync(); // возвращаем заголовки как поток
        }

        private static IEnumerable<string> GetDataLines()
        {
            /// 
            /// Данная функция позволяет читать не всю информацию, а только нужные блоки. В случае необходимости, чтение потока можно прервать
            /// 

            using var data_stream = GetDataStream().Result; // отправляем запрос к серверу с помощью функции подключения
            using var data_reader = new StreamReader(data_stream); // читаем данные из потока

            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine(); //считываем строку из потока
                if (string.IsNullOrWhiteSpace(line)) continue;  // проверяем на пустое содержимое
                yield return line.Replace("Korea,", "Korea -").Replace("Bonaire,", "Bonaire -").Replace("Helena,", "Helena -"); // возвращаем строку как результат, возвращаем генератором
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();


        private static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
        {
            var lines = GetDataLines()
                .Skip(1) // skip - отбрасывает первую строку, т.к. это заголовок
                .Select(lines => lines.Split(','));

            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"');
                var counts = row.Skip(4).Select(int.Parse).ToArray();


                yield return (country_name, province, counts);
            }

        }

        static void Main(string[] args)
        {
            //WebClient client = new WebClient()

            //var client = new HttpClient();

            //var responce = client.GetAsync(data_url).Result;
            //var csv_str = responce.Content.ReadAsStringAsync().Result;

            //foreach (var data_line in GetDataLines())
            //{
            //    Console.WriteLine(data_line);
            //}

            //var dates = GetDates();

            //Console.WriteLine(string.Join("\r\n", dates));

            var ukraine_data = GetData().First(v => v.Country.Equals("Ukraine", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(string.Join("\r\n", GetDates().Zip(ukraine_data.Counts, (date, count) => $"{date:d} - {count}")));


            Console.ReadLine();
        }
    }
}
