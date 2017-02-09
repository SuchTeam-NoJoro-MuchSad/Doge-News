using DogeNews.Web.Models;

namespace DogeNews.Web.Services.Contracts
{
    public interface IArticleManagementService
    {
        void Add(string username, NewsWebModel newsItem);

        void Delete(string newsItemId);

        void Restore(string newsItemId);
    }
}