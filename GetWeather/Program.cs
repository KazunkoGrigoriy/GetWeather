using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace GetWeather
{
    class Program
    {

        static void Main(string[] args)
         {
            Console.Write("Введите название города: "); string town = Console.ReadLine();
            Console.WriteLine();
            string url = "https://api.openweathermap.org/data/2.5/weather?q=" + $"{town}" + "&units=metric&appid=" + "8a424f124eedd1c859d12e03fff36147";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            WeatherResponse weather = JsonConvert.DeserializeObject<WeatherResponse>(response);

            Console.WriteLine($"Температура воздуха в городе {town} {weather.Main.Temp} градусов");
            Console.WriteLine($"Влажность воздуха {weather.Main.Humidity} %");
            Console.WriteLine($"Закат {weather.Sys.Sunrise}");         
            Console.WriteLine($"Восход {weather.Sys.Sunset}");

            string date = DateTime.Now.ToShortDateString();
            
            using (StreamWriter streamWriter = new StreamWriter(@$"{date.Replace('.','_')}.txt", true))
            {
                streamWriter.WriteLine($"Температура воздуха в городе {town} {weather.Main.Temp} градусов");
                streamWriter.WriteLine($"Влажность воздуха {weather.Main.Humidity} %");
                streamWriter.WriteLine($"Закат {weather.Sys.Sunrise}");
                streamWriter.WriteLine($"Восход {weather.Sys.Sunset}");
            }

            Console.WriteLine("Данные записаны в файл");
        }
    }
}
