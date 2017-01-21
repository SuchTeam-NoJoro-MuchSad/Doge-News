﻿using System;

using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Services
{
    public class CryptographicService : ICryptographicService
    {
        private const int HashIterations = 10000;
        private const int SaltBytes = 23;
        private const int HashBytes = 29;

        private readonly ICryptoServiceSaltProvider saltService;
        private readonly ICryptoServiceHashProvider hashProvider;

        public CryptographicService(ICryptoServiceSaltProvider saltService, ICryptoServiceHashProvider hashProvider)
        {
            this.saltService = saltService;
            this.hashProvider = hashProvider;
        }

        public byte[] GetSalt()
        {
            byte[] salt = new byte[SaltBytes];
            salt = this.saltService.GetBytes(salt);
            return salt;
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            byte[] passHash = this.hashProvider.GetHashBytes(password, salt, HashIterations, HashBytes);
            return passHash;
        }

        public string ByteArrayToString(byte[] array)
        {
            string result = Convert.ToBase64String(array);
            return result;
        }

        public bool IsValidPassword(string passwordToCheck, string userPashHash, string userSalt)
        {
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
    }
}
