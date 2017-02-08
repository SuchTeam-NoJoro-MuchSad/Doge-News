using System.Web;
using DogeNews.Common.Enums;

namespace DogeNews.Web.Mvp.News.Add.EventArguments
{
    public class AddNewsEventArgs
    {
        public string Title { get; set; }

        public HttpPostedFile Image { get; set; }

        public string FileName { get; set; }

        public string Content { get; set; }

        public NewsCategoryType Category { get; set; }
    }
}