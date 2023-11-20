using Redis.Services.DateBaseModels;

namespace Redis.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(int userId);
    }
}
