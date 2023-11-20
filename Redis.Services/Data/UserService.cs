using Redis.Services.Contracts;
using Redis.Services.DateBaseModels;
using Redis.Services.Interfaces;

namespace Redis.Services.Data
{
    public class UserService : IUserService
    {
        private readonly IUserRepository? _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetUser(int userId)
        {
            return await _userRepository.GetOrCreateUser(userId);
        }
    }
}
