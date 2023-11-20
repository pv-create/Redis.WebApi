using Microsoft.Extensions.Caching.Distributed;
using Redis.Db.Repositories;
using Redis.Services.Contracts;
using Redis.Services.DateBaseModels;
using System.Text.Json;

namespace Redis.Db.CasheRepository
{
    public class UserCasheRepository : IUserRepository
    {
        private readonly IDistributedCache _distributedCache;

        private readonly UserRepository _userRepository;

        public UserCasheRepository(
            IDistributedCache casDistributedCache,
            UserRepository userRepository)
        {
            _distributedCache = casDistributedCache;
            _userRepository = userRepository;
        }

        public async Task<User?> GetOrCreateUser(int id)
        {
            var userString = await _distributedCache.GetStringAsync($"user:{id.ToString()}");

            if (userString != null) return JsonSerializer.Deserialize<User>(userString);

            else
            {
                User userFromDb = _userRepository.GetUser(id);

                userString = JsonSerializer.Serialize(userFromDb);

                await _distributedCache.SetStringAsync($"user:{userFromDb.Id.ToString()}", userString, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                });

                return userFromDb;
            }
        }
    }
}
