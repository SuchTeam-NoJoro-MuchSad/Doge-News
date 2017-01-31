using DogeNews.Data.Models;
using DogeNews.Web.Models;

namespace DogeNews.Web.Services.Contracts
{
    public interface INewsDataSourceService : IDataSourceService<NewsItem, NewsWebModel>
    {
        NewsWebModel GetItemByTitle(string title);
    }
}