using System.Collections.Generic;

using DogeNews.Web.Models;

namespace DogeNews.Web.Services.Contracts
{
    public interface INewsService
    {
        NewsWebModel GetItemByTitle(string title);

        NewsWebModel GetItemById(string id);

        IEnumerable<NewsWebModel> GetSliderNews();

        IEnumerable<NewsWebModel> GetNewsItemsByCategory(string category);
    }
}
