using System;
using System.IO;

using DogeNews.Web.Services.Contracts;

namespace DogeNews.Web.Services
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
            var fileExtension = Path.GetExtension(path);
            return fileExtension;
        }

        public string GetUniqueFileName(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            var guid = Guid.NewGuid().ToString();
            var fileName = $"{username}{guid}"
                .Replace(' ', '-')
                .Replace(':', '-')
                .Replace('.', '-')
                .Replace('@', '-');

            return fileName;
        }
    }
}
