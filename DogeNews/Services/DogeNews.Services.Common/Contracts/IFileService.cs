namespace DogeNews.Services.Common.Contracts
{
    public interface IFileService
    {
        string GetUniqueFileName(string username);

        void CreateFile(string folderName, string fileName);

        string GetFileExtension(string path);
    }
}
