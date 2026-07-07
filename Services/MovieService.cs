using System.Text.Json;

namespace FilmLogAPI.Services
{
    public class MovieService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;

        public MovieService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = config["OMDb:ApiKey"]!;
        }

        public async Task<JsonElement> SearchMovie(string title)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"https://www.omdbapi.com/?s={title}&apikey={_apiKey}";
            var response = await client.GetStringAsync(url);
            return JsonSerializer.Deserialize<JsonElement>(response);
        }
    }
}