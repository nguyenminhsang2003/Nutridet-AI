using Microsoft.EntityFrameworkCore;
using nutridet_ai_api.Models;
using nutridet_ai_api.Repositories.IRepositories;

namespace nutridet_ai_api.Repositories
{
    public class UserReponsitory : IUserReponsitory
    {
        private readonly NutridetAiDbContext _context;
        public UserReponsitory(NutridetAiDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
