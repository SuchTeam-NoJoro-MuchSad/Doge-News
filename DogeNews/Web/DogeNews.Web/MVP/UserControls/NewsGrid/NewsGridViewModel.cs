using System.Collections.Generic;

using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.Services.Contracts;

namespace DogeNews.Web.MVP.UserControls.NewsGrid
{
    public class NewsGridViewModel
    {
        public int NewsCount { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<NewsWebModel> CurrentPageNews { get; set; }

        public IDataSourceService<NewsItem, NewsWebModel> NewsDataSource { get; set; }
    }
}