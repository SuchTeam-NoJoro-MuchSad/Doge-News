using System.Collections.Generic;

using DogeNews.Web.Models;

namespace DogeNews.Web.Services.Contracts
{
    public interface INewsService
    {
        void Add(string username, NewsWebModel newsItem);

        NewsWebModel GetItemByTitle(string title);

        IEnumerable<NewsWebModel> GetSliderNews();
    }
}
