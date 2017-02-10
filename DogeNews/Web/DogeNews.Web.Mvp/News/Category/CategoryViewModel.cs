using System.Collections.Generic;

using DogeNews.Web.Models;

namespace DogeNews.Web.Mvp.News.Category
{
    public class CategoryViewModel
    {
        public IEnumerable<NewsWebModel> News { get; set; }
    }
}