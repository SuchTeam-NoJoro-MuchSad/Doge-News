using System.Web.UI;

namespace DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments
{
    public class PageLoadEventArgs
    {
        public bool IsAdminUser { get; set; }

        public bool IsPostBack { get; set; }

        public string QueryString { get; set; }

        public StateBag ViewState { get; set; }
    }
}