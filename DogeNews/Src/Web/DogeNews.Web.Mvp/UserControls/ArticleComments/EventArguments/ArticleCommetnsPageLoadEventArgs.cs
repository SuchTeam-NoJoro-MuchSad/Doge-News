using System.Web.UI;

namespace DogeNews.Web.Mvp.UserControls.ArticleComments.EventArguments
{
    public class ArticleCommetnsPageLoadEventArgs
    {
        public bool IsPostBack { get; set; }

        public StateBag ViewState { get; set; }

        public string Title { get; set; }
    }
}