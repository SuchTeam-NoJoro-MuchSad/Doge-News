using System.Web.UI;

namespace DogeNews.Web.Mvp.News.Article.EventArguments
{
    public class ArticlePageLoadEventArgs
    {
        public bool IsPostBack { get; set; }

        public string QueryString { get; set; }

        public StateBag ViewState { get; set; }
    }
}