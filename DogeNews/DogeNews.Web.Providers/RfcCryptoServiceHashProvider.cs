using System.Security.Cryptography;

using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Providers
{
    public class RfcCryptoServiceHashProvider : ICryptoServiceHashProvider
    {
        public byte[] GetHashBytes(string stringToHash, byte[] salt, int iterationsCount, int hashBytesCount)
        {
            var bytes = new Rfc2898DeriveBytes(stringToHash, salt, iterationsCount);
            byte[] hash = bytes.GetBytes(hashBytesCount);
            return hash;
        }
    }
}
