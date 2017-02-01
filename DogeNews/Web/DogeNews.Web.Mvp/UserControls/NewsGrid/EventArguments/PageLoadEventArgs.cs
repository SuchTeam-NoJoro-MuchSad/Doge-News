using System.Web.UI;

namespace DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments
{
    public class PageLoadEventArgs
    {
        public bool IsPostBack { get; set; }

        public StateBag ViewState { get; set; }
    }
}