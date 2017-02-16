using System.Collections.Generic;
using DogeNews.Web.Models;

namespace DogeNews.Services.Data.Contracts
{
    public interface INewsService
    {
        NewsWebModel GetItemByTitle(string title);

        NewsWebModel GetItemById(int id);

        IEnumerable<NewsWebModel> GetSliderNews();

        IEnumerable<NewsWebModel> GetNewsItemsByCategory(string category);
    }
}
