using nutridet_ai_api.DTO;
using nutridet_ai_api.Repositories.IRepositories;
using nutridet_ai_api.Services.IService;

namespace nutridet_ai_api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserReponsitory _userReponsitory;
        public UserService(IUserReponsitory userReponsitory)
        {
            _userReponsitory = userReponsitory;
        }
        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            var userExit = await _userReponsitory.GetUserForLoginAsync(email,password);
            if (userExit != null)
            {
                return  new LoginResponse { Result = true, User = userExit };
            }
            return new LoginResponse
            {
                Result = false
            };
        }
    }
}
