namespace DogeNews.Web.Providers.Contracts
{
    public interface IFileProvider
    {
        string GetUniqueFileName(string username);

        void CreateFile(string folderName, string fileName);
    }
}
