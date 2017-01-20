﻿using System.Security.Cryptography;

using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Providers
{
    public class RngCryptoServiceSaltProvider : ICryptoServiceSaltProvider
    {
        public byte[] GetBytes(byte[] array)
        {
            new RNGCryptoServiceProvider().GetBytes(array);
            return array;
        }
    }
}