using System;
using System.Security.Cryptography;

using DogeNews.Web.Services.Contracts;

namespace DogeNews.Web.Services
{
    public class CryptographicService : ICryptographicService
    {
        private const int HashIterations = 10000;
        private const int SaltBytes = 251;
        private const int HashBytes = 257;

        public byte[] GetSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltBytes]);
            return salt;
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            var bytes = new Rfc2898DeriveBytes(password, salt, HashIterations);
            byte[] passHash = bytes.GetBytes(HashBytes);
            return passHash;
        }

        public string ByteArrayToString(byte[] array)
        {
            string result = Convert.ToBase64String(array);
            return result;
        }

        public bool IsValidPassword(string passwordToCheck, string passHash, string salt)
        {
            byte[] hashBytes = Convert.FromBase64String(passHash);
            byte[] saltBytes = Convert.FromBase64String(salt);
            var newPassHashBytes = new Rfc2898DeriveBytes(passwordToCheck, saltBytes, HashIterations);
            byte[] newPassHash = newPassHashBytes.GetBytes(HashBytes);

            for (int i = 0; i < HashBytes; i++)
            {
                if (hashBytes[i + SaltBytes] != newPassHash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
