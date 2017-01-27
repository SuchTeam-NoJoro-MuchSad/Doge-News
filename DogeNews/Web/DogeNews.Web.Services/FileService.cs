﻿using System;
using System.IO;

using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Services
{
    public class FileService : IFileService
    {
        private readonly IDateTimeProvider dateTimeProvider;

        public FileService(IDateTimeProvider dateTimeProvider)
        {
            this.ValidateConstructorParams(dateTimeProvider);

            this.dateTimeProvider = dateTimeProvider;
        }

        public void CreateFile(string folderName, string fileName)
        {
            if (!Directory.Exists($"{folderName}"))
            {
                Directory.CreateDirectory($"{folderName}");
            }

            File.Create($"{folderName}\\{fileName}").Dispose();
        }

        public string GetUniqueFileName(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            var guid = Guid.NewGuid().ToString();
            var now = this.dateTimeProvider.Now.ToString().Replace('/', '-');
            var fileName = $"{username}{guid}{now}"
                .Replace(' ', '-')
                .Replace(':', '-');

            return fileName;
        }

        private void ValidateConstructorParams(IDateTimeProvider dateTimeProvider)
        {
            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException(nameof(dateTimeProvider));
            }
        }
    }
}