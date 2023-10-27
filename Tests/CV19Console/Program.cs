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
                yield return line; // возвращаем строку как результат, возвращаем генератором
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();


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

            var dates = GetDates();

            Console.WriteLine(string.Join("\r\n", dates));

        }
    }
}
