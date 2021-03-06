using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MicroservicoAPI
{
    public class WeatherClient
    {
        private readonly HttpClient httpClient;
        public readonly ServiceSettings settings;

        public WeatherClient(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            this.httpClient = httpClient;
            settings = options.Value;
        }

        public record Weather(string description);
        public record Main(decimal temp);
        public record Forecast(Weather[] Weather, Main main, long dt);

        public async Task<Forecast> GetCurrentWeatherAsync(string city)
        {
            var forecast = await httpClient.GetFromJsonAsync<Forecast>(
                $"https://{settings.OpenWeatherHost}/data/2.5/weather?q={city}&appid={settings.ApiKey}&units=metric"
                );
            return forecast;
        }
    }
}
