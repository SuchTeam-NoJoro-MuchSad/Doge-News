namespace DogeNews.Web.Services.Contracts
{
    public interface IFileService
    {
        string GetUniqueFileName(string username);

        void CreateFile(string folderName, string fileName);
    }
}
