using DogeNews.Web.Models;
using DogeNews.Web.Services.Contracts;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Providers.Contracts;

using System.Web;

namespace DogeNews.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> userRepository;
        private readonly INewsData newsData;
        private readonly ICryptographicService cryptographicService;
        private readonly IMapperProvider mapperProvider;

        public AuthService(
            IRepository<User> userRepository,
            INewsData newsData,
            ICryptographicService cryptographicService,
            IMapperProvider mapperProvider)
        {
            this.userRepository = userRepository;
            this.newsData = newsData;
            this.cryptographicService = cryptographicService;
            this.mapperProvider = mapperProvider;
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
            var passHash = this.cryptographicService.HashPassword(user.Password, salt);
            var newUser = this.mapperProvider.Instance.Map<User>(user);

            newUser.Salt = this.cryptographicService.ByteArrayToString(salt);
            newUser.PassHash = this.cryptographicService.ByteArrayToString(passHash);

            this.userRepository.Add(newUser);
            this.newsData.Commit();
            return true;
        }

        public UserWebModel LoginUser(string username, string password)
        {
            var foundUser = this.userRepository.GetFirst(u => u.Username == username);
            if (foundUser == null)
            {
                // no such registered user
                return null;
            }

            bool isValidPassword = this.cryptographicService.IsValidPassword(password, foundUser.PassHash, foundUser.Salt);
            if (!isValidPassword)
            {
                // incorrect password
                return null;
            }

            var result = this.mapperProvider.Instance.Map<UserWebModel>(foundUser);
            return result;
        }

        public void GetLoggedInUserInformation(HttpCookie cookie)
        {
            var username = cookie["username"];
            var userId = cookie["id"];
        }
    }
}
