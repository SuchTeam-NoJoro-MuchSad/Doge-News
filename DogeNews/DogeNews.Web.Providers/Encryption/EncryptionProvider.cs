using System;
using System.Text;
using System.Web.Security;

using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Providers.Encryption
{
    public class EncryptionProvider : IEncryptionProvider
    {
        public string Encrypt(string text, string key)
        {
            this.ValidateParams(text, key);

            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            byte[] encryptedTextBytes = MachineKey.Protect(textBytes, key);
            string encryptedText = Convert.ToBase64String(encryptedTextBytes);

            return encryptedText;
        }

        public string Decrypt(string text, string key)
        {
            this.ValidateParams(text, key);

            byte[] textBytes = Convert.FromBase64String(text);
            byte[] decryptedBytes = MachineKey.Unprotect(textBytes, key);
            string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

            return decryptedText;
        }

        private void ValidateParams(string text, string key)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
        }
    }
}
