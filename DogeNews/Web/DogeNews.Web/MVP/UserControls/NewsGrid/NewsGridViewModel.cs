using DogeNews.Web.Models;

using System.Collections.Generic;

namespace DogeNews.Web.MVP.UserControls.NewsGrid
{
    public class NewsGridViewModel
    {
        public int NewsCount { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<NewsWebModel> CurrentPageNews { get; set; }
    }
}