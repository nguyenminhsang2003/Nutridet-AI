namespace nutridet_ai_api.Services.IService
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string role);
    }
}
