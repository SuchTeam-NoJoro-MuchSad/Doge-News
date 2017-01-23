using System;

using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Services
{
    public class CryptographicService : ICryptographicService
    {
        private const int HashIterations = 10000;
        private const int SaltBytes = 23;
        private const int HashBytes = 29;

        private readonly ICryptoServiceSaltProvider saltProvider;
        private readonly ICryptoServiceHashProvider hashProvider;

        public CryptographicService(ICryptoServiceSaltProvider saltProvider, ICryptoServiceHashProvider hashProvider)
        {
            this.ValidateConstructorParams(saltProvider, hashProvider);

            this.saltProvider = saltProvider;
            this.hashProvider = hashProvider;
        }

        public byte[] GetSalt()
        {
            byte[] salt = new byte[SaltBytes];
            salt = this.saltProvider.GetBytes(salt);
            return salt;
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            if (salt == null)
            {
                throw new ArgumentNullException("salt");
            }

            byte[] passHash = this.hashProvider.GetHashBytes(password, salt, HashIterations, HashBytes);
            return passHash;
        }

        public string ByteArrayToString(byte[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            string result = Convert.ToBase64String(array);
            return result;
        }

        public bool IsValidPassword(string passwordToCheck, string userPashHash, string userSalt)
        {
            if (string.IsNullOrEmpty(passwordToCheck))
            {
                throw new ArgumentNullException("passwordToCheck");
            }

            if (string.IsNullOrEmpty(userPashHash))
            {
                throw new ArgumentNullException("userPassHash");
            }

            if (string.IsNullOrEmpty(userSalt))
            {
                throw new ArgumentNullException("userSalt");
            }

            byte[] hashBytes = Convert.FromBase64String(userPashHash);
            byte[] saltBytes = Convert.FromBase64String(userSalt);
            var newPassHashBytes = this.hashProvider.GetHashBytes(passwordToCheck, saltBytes, HashIterations, HashBytes);
            
            for (int i = 0; i < HashBytes; i++)
            {
                if (hashBytes[i] != newPassHashBytes[i])
                {
                    return false;
                }
            }

            return true;
        }

        private void ValidateConstructorParams(
            ICryptoServiceSaltProvider saltProvider, 
            ICryptoServiceHashProvider hashProvider)
        {
            if (saltProvider == null)
            {
                throw new ArgumentNullException("saltProvider");
            }

            if (hashProvider == null)
            {
                throw new ArgumentNullException("hashProvider");
            }
        }
    }
}
