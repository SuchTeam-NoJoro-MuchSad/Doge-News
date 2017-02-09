using System.Web;
using DogeNews.Common.Enums;

namespace DogeNews.Web.Mvp.News.Edit.EventArguments
{
    public class EditArticleEventArgs
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public HttpPostedFile Image { get; set; }

        public string FileName { get; set; }

        public string Content { get; set; }

        public NewsCategoryType Category { get; set; }
    }
}