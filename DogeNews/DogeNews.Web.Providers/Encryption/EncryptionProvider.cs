using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.IO;

using DogeNews.Web.Providers.Contracts;

// CREDITS: http://stackoverflow.com/questions/10168240/encrypting-decrypting-a-string-in-c-sharp
namespace DogeNews.Web.Providers.Encryption
{
    public class EncryptionProvider : IEncryptionProvider
    {
        private const int RandomBitsCount = 32;
        private const int Keysize = 256;
        private const int DerivationIterations = 1000;
        private const int ByteSize = 8;

        private byte[] randomBytes;

        public EncryptionProvider()
        {
            this.randomBytes = new byte[RandomBitsCount];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(randomBytes);
            }
        }

        public string Encrypt(string text, string key)
        {
            var saltStringBytes = this.randomBytes;
            var ivStringBytes = this.randomBytes;
            var plainTextBytes = Encoding.UTF8.GetBytes(text);

            using (var password = new Rfc2898DeriveBytes(key, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / ByteSize);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = Keysize;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;

                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();

                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();

                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public string Decrypt(string text, string key)
        {
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(text);
            var saltStringBytes = cipherTextBytesWithSaltAndIv
                .Take(Keysize / ByteSize)
                .ToArray();
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / ByteSize)
                .Take(Keysize / ByteSize)
                .ToArray();
            var cipherTextBytes = cipherTextBytesWithSaltAndIv
                .Skip((Keysize / ByteSize) * 2)
                .Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / ByteSize) * 2))
                .ToArray();

            using (var password = new Rfc2898DeriveBytes(key, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / ByteSize);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = Keysize;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;

                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                                memoryStream.Close();
                                cryptoStream.Close();

                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }
    }
}
