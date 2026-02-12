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
Hãy phân tích hình ảnh và trả về giá trị dinh dưỡng cho TOÀN BỘ sản phẩm (không phải cho 100ml hay đơn vị tính khác).

Yêu cầu:
1. Đọc và trích xuất TẤT CẢ các thông tin dinh dưỡng có trong hình ảnh (nhãn sản phẩm, bảng giá trị dinh dưỡng)
2. Tìm thể tích/khối lượng thực tế của sản phẩm (ví dụ: ""Thể tích thực: 600 ml"", ""Net volume: 330 ml"", ""Khối lượng tịnh: 500g""). Nếu không tìm thấy trực tiếp, hãy suy luận từ kích thước sản phẩm (chai nước ngọt thường là 330ml, 500ml, 600ml, 1L)
3. Tính toán giá trị dinh dưỡng cho TOÀN BỘ sản phẩm:
   - Nếu giá trị dinh dưỡng trên nhãn là ""trên 100 ml"" và thể tích thực là 600 ml, thì tính: giá trị trên 100ml × (600/100) = giá trị cho toàn bộ sản phẩm
   - Ví dụ: Năng lượng 42 kcal/100ml × 6 = 252 kcal cho 600ml
   - Áp dụng công thức này cho TẤT CẢ các chất dinh dưỡng
4. Kết quả trả về theo form kết quả theo định dạng Markdown với cấu trúc:
   - Tiêu đề (##) cho tên sản phẩm
   - **Thể tích thực sản phẩm:** [giá trị]
   - **Giá trị dinh dưỡng cho toàn bộ sản phẩm:**
     * Các vitamin và khoáng chất khác (nếu có)
5. QUAN TRỌNG: 
   - CHỈ hiển thị giá trị dinh dưỡng cho TOÀN BỘ sản phẩm, KHÔNG hiển thị giá trị cho 100ml hay đơn vị tính khác
   - Tất cả các giá trị dinh dưỡng phải được tính toán dựa trên thể tích thực sản phẩm
   - Làm tròn số đến 1 chữ số thập phân (ví dụ: 252.0 kcal, 63.0 g)

Nếu không tìm thấy thông tin dinh dưỡng trong hình, hãy thông báo rõ ràng.";

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
