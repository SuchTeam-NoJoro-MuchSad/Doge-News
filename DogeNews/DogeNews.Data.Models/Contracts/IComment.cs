namespace DogeNews.Data.Models.Contracts
{
    public interface IComment
    {
        int Id { get; set; }
        User User { get; set; }
        string Content { get; set; }
    }
}