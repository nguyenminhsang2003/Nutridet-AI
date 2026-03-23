using nutridet_ai_api.Models;

namespace nutridet_ai_api.DTO
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class LoginResponse
    {
        public bool Result { get; set; }
        public User? User { get; set; }
    }
    public class FilterInvoke
    {
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? page { get; set; }
        public int? pageSize { get; set; }
    }
}
