using System.Collections.Generic;

using DogeNews.Web.Models;

namespace DogeNews.Web.Mvp.Default
{
    public class DefaultViewModel
    {
        public IEnumerable<NewsWebModel> SliderNews { get; set; }
    }
}