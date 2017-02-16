using System;
using System.IO;

using DogeNews.Services.Common.Contracts;

namespace DogeNews.Services.Common
{
    public class FileService : IFileService
    {
        public void CreateFile(string folderName, string fileName)
        {
            if (!Directory.Exists($"{folderName}"))
            {
                Directory.CreateDirectory($"{folderName}");
            }

            File.Create($"{folderName}\\{fileName}").Dispose();
        }

        public string GetFileExtension(string path)
        {
            string fileExtension = Path.GetExtension(path);
            return fileExtension;
        }

        public string GetUniqueFileName(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            string guid = Guid.NewGuid().ToString();
            string fileName = $"{username}{guid}"
                .Replace(' ', '-')
                .Replace(':', '-')
                .Replace('.', '-')
                .Replace('@', '-');

            return fileName;
        }
    }
}
