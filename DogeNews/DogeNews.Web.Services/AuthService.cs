using DogeNews.Web.Models;
using DogeNews.Web.Services.Contracts;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Providers;

namespace DogeNews.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> userRepository;
        private readonly INewsData newsData;
        private readonly ICryptographicService cryptographicService;

        public AuthService(
            IRepository<User> userRepository,
            INewsData newsData, 
            ICryptographicService cryptographicService)
        {
            this.userRepository = userRepository;
            this.newsData = newsData;
            this.cryptographicService = cryptographicService;
        }

        public bool RegisterUser(UserWebModel user)
        {
            var foundUser = this.userRepository.GetFirst(u => u.Username == user.Username);

            if (foundUser != null)
            {
                // the user already exists
                return false;
            }

            var salt = this.cryptographicService.GetSalt();
            var saltString = this.cryptographicService.ByteArrayToString(salt);
            var passHash = this.cryptographicService.HashPassword(user.Password, salt);
            var passHashString = this.cryptographicService.ByteArrayToString(passHash);
            var newUser = MapperProvider.Instance.Map<User>(user);
            newUser.Salt = saltString;
            newUser.PassHash = passHashString;

            this.userRepository.Add(newUser);
            this.newsData.Commit();
            return true;
        }
    }
}
