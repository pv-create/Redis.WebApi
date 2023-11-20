using Redis.Services.DateBaseModels;

namespace Redis.Services.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetOrCreateUser(int id);
    }
}
