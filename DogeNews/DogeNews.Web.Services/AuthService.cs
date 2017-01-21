using System.Web;
using System.Web.Configuration;

using DogeNews.Web.Models;
using DogeNews.Web.Services.Contracts;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> userRepository;
        private readonly INewsData newsData;
        private readonly ICryptographicService cryptographicService;
        private readonly IMapperProvider mapperProvider;
        private readonly IEncryptionProvider encryptionProvider;

        public AuthService(
            IRepository<User> userRepository,
            INewsData newsData,
            ICryptographicService cryptographicService,
            IMapperProvider mapperProvider,
            IEncryptionProvider encryptionProvider)
        {
            this.userRepository = userRepository;
            this.newsData = newsData;
            this.cryptographicService = cryptographicService;
            this.mapperProvider = mapperProvider;
            this.encryptionProvider = encryptionProvider;
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
                // no such user
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

        public bool IsUserLoggedIn(HttpCookieCollection cookies, string authCookieName, string encryptionKey)
        {
            var cookie = cookies[authCookieName];

            if (cookie == null)
            {
                return false;
            }
            
            string usernameKey = this.encryptionProvider.Encrypt("Username", encryptionKey);
            string idKey = this.encryptionProvider.Encrypt("Id", encryptionKey);

            if (cookie[usernameKey] == null || cookie[idKey] == null)
            {
                return false;
            }

            return true;
        }
    }
}
