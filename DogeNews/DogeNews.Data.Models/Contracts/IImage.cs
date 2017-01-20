namespace DogeNews.Data.Models.Contracts
{
    public interface IImage
    {
        int Id { get; set; }
        string Name { get; set; }
        string FullName { get; set; }
        string Url { get; set; }
        string FileExtention { get; set; }
    }
}