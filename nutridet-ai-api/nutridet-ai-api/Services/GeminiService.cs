using nutridet_ai_api.Services.IService;
using System.Text;
using System.Text.Json;

namespace nutridet_ai_api.Services
{
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Gemini:ApiKey"];
        }

        public async Task<string> GenerateAsync(string prompt)
        {
            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                new
                {
                    role = "user",
                    parts = new[]
                    {
                        new { text = prompt + "\n giá trị dinh dưỡng có trong hình"}
                    }
                }
            }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await Task.Delay(1500);
            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);

            return doc.RootElement
                      .GetProperty("candidates")[0]
                      .GetProperty("content")
                      .GetProperty("parts")[0]
                      .GetProperty("text")
                      .GetString() ?? string.Empty;
        }
    }
}
