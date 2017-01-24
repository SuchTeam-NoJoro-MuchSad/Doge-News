using System;

using DogeNews.Web.Services.Contracts;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;

namespace DogeNews.Web.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly ICryptographicService cryptographicService;
        private readonly INewsData newsData;

        public UserService(
            IRepository<User> userRepository,
            ICryptographicService cryptographicService,
            INewsData newsData)
        {
            this.ValidateConstructorParams(userRepository, cryptographicService, newsData);

            this.userRepository = userRepository;
            this.cryptographicService = cryptographicService;
            this.newsData = newsData;
        }

        public void ChangePassword(string username, string newPassword)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentNullException("newPassword");
            }

            var user = this.userRepository.GetFirst(x => x.Username == username);
            var salt = this.cryptographicService.Base64StringToByteArray(user.Salt);
            var newPasshashBytes = this.cryptographicService.HashPassword(newPassword, salt);
            var newPassHash = this.cryptographicService.ByteArrayToString(newPasshashBytes);

            user.PassHash = newPassHash;
            this.newsData.Commit();
        }

        private void ValidateConstructorParams(
            IRepository<User> userRepository,
            ICryptographicService cryptographicService,
            INewsData newsData)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            if (cryptographicService == null)
            {
                throw new ArgumentNullException("cryptographicService");
            }

            if (newsData == null)
            {
                throw new ArgumentNullException("newsData");
            }
        }
    }
}
