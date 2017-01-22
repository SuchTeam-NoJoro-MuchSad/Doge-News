namespace DogeNews.Web.Providers.Contracts
{
    public interface IFileProvider
    {
        string GetUnique(string username);

        void CreateFile(string folderName, string fileName);
    }
}
