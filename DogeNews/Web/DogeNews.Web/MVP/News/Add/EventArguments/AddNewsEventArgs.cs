using System.Web;

using DogeNews.Web.Common.Enums;

namespace DogeNews.Web.MVP.News.Add.EventArguments
{
    public class AddNewsEventArgs
    {
        public string Title { get; set; }

        public HttpPostedFile Image { get; set; }

        public string Content { get; set; }

        public NewsCategoryType Category { get; set; }
    }
}