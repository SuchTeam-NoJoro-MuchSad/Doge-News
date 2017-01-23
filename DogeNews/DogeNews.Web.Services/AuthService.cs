using System;
using System.Web;

using DogeNews.Web.Models;
using DogeNews.Web.Services.Contracts;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Common.Enums;

namespace DogeNews.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> userRepository;
        private readonly INewsData newsData;
        private readonly ICryptographicService cryptographicService;
        private readonly IMapperProvider mapperProvider;
        private readonly IEncryptionProvider encryptionProvider;
        private readonly IAppConfigurationProvider configProvider;

        public AuthService(
            IRepository<User> userRepository,
            INewsData newsData,
            ICryptographicService cryptographicService,
            IMapperProvider mapperProvider,
            IEncryptionProvider encryptionProvider,
            IAppConfigurationProvider configProvider)
        {
            this.ValidateConstructorParams(
                userRepository, 
                newsData, 
                cryptographicService, 
                mapperProvider, 
                encryptionProvider, 
                configProvider);

            this.userRepository = userRepository;
            this.newsData = newsData;
            this.cryptographicService = cryptographicService;
            this.mapperProvider = mapperProvider;
            this.encryptionProvider = encryptionProvider;
            this.configProvider = configProvider;
        }

        public bool RegisterUser(UserWebModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

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
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

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

        public bool IsUserLoggedIn(HttpCookieCollection cookies)
        {
            if (cookies == null)
            {
                throw new ArgumentNullException("cookies");
            }

            var cookie = cookies[this.configProvider.AuthCookieName];

            if (cookie == null)
            {
                return false;
            }

            string usernameKey = this.encryptionProvider.Encrypt("Username", this.configProvider.EncryptionKey);
            string idKey = this.encryptionProvider.Encrypt("Id", this.configProvider.EncryptionKey);

            if (cookie[usernameKey] == null || cookie[idKey] == null)
            {
                return false;
            }

            return true;
        }

        public void SeedAdminUser()
        {
            var foundUser = this.userRepository.GetFirst(x => x.Username == "Admin");

            if (foundUser == null)
            {
                var saltBytes = this.cryptographicService.GetSalt();
                var passHashBytes = this.cryptographicService.HashPassword(this.configProvider.AdminPassword, saltBytes);
                var salt = this.cryptographicService.ByteArrayToString(saltBytes);
                var passHash = this.cryptographicService.ByteArrayToString(passHashBytes);

                var adminUser = new User
                {
                    Username = "Admin",
                    Email = "admin@admin.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserRole = UserRoleType.Admin,
                    Salt = salt,
                    PassHash = passHash
                };

                this.userRepository.Add(adminUser);
                this.newsData.Commit();
            }
        }

        public void LogoutUser(HttpCookieCollection cookies)
        {
            if (cookies == null)
            {
                throw new ArgumentNullException("cookies");
            }

            var cookieName = this.configProvider.AuthCookieName;
            var cookie = cookies.Get(cookieName);

            if (cookie == null)
            {
                return;
            }

            cookie.Expires = DateTime.Now;
            cookies.Set(cookie);
        }

        private void ValidateConstructorParams(
            IRepository<User> userRepository,
            INewsData newsData,
            ICryptographicService cryptographicService,
            IMapperProvider mapperProvider,
            IEncryptionProvider encryptionProvider,
            IAppConfigurationProvider configProvider)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            if (newsData == null)
            {
                throw new ArgumentNullException("newsData");
            }

            if (cryptographicService == null)
            {
                throw new ArgumentNullException("cryptographicService");
            }

            if (mapperProvider == null)
            {
                throw new ArgumentNullException("mapperProvider");
            }

            if (encryptionProvider == null)
            {
                throw new ArgumentNullException("encryptionProvider");
            }

            if (configProvider == null)
            {
                throw new ArgumentNullException("configProvider");
            }
        }
    }
}
