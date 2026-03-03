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

        public async Task<string> GenerateAsync(string imageDataUrl)
        {
            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";

            // Parse image data URL to extract mime type and base64 data
            string mimeType = "image/jpeg";
            string base64Data = imageDataUrl;

            if (imageDataUrl.StartsWith("data:"))
            {
                var parts = imageDataUrl.Split(',');
                if (parts.Length == 2)
                {
                    var mimePart = parts[0].Replace("data:", "").Split(';')[0];
                    mimeType = mimePart;
                    base64Data = parts[1];
                }
            }

            string nutritionPrompt = @"
You are an AI specialized in extracting nutrition information from food labels (image or text).

Your task is to read the nutrition label and return structured nutrition data.

STRICT RULES:
1. Return ONLY valid JSON. Do not include explanations, markdown, or extra text.
2. The JSON must ALWAYS contain all fields listed in the schema.
3. If a value is missing or not visible in the label, return null.
4. All numbers must be decimal numbers (no units in values).
5. Extract values exactly as written on the label (do NOT calculate or convert unless explicitly shown).
6. Units must be interpreted as:
   - kcal → energyKcal
   - g → carbohydrateG, sugarG, proteinG, fatG, saturatedFatG, fiberG
   - mg → sodiumMg, cholesterolMg
7.If multiple nutrition tables exist, prioritize the table labeled ""per 100g"" or ""per 100ml"".
LANGUAGE SUPPORT:
The nutrition label may be in Vietnamese or English.

Vietnamese mapping:
- Năng lượng → energyKcal
- Carbohydrate / Carb / Tinh bột → carbohydrateG
- Đường → sugarG
- Chất đạm / Protein → proteinG
- Chất béo / Fat → fatG
- Chất béo bão hòa → saturatedFatG
- Chất xơ → fiberG
- Natri / Muối → sodiumMg
- Cholesterol → cholesterolMg

JSON SCHEMA (must match exactly):

{
  ""energyKcal"": number | null,
  ""carbohydrateG"": number | null,
  ""sugarG"": number | null,
  ""proteinG"": number | null,
  ""fatG"": number | null,
  ""saturatedFatG"": number | null,
  ""fiberG"": number | null,
  ""sodiumMg"": number | null,
  ""cholesterolMg"": number | null
}

EXAMPLE OUTPUT:

{
  ""energyKcal"": 42,
  ""carbohydrateG"": 10.5,
  ""sugarG"": 10.5,
  ""proteinG"": 0,
  ""fatG"": 0,
  ""saturatedFatG"": null,
  ""fiberG"": null,
  ""sodiumMg"": 23,
  ""cholesterolMg"": null
}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        role = "user",
                        parts = new object[]
                        {
                            new { 
                                inline_data = new
                                {
                                    mime_type = mimeType,
                                    data = base64Data
                                }
                            },
                            new { text = nutritionPrompt }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var geminiJson = JsonDocument.Parse(responseString);

            var text = geminiJson
                .RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            if (text.StartsWith("```"))
            {
                text = text.Replace("```json", "")
                           .Replace("```", "")
                           .Trim();
            }

            return text;
        }
    }
}
