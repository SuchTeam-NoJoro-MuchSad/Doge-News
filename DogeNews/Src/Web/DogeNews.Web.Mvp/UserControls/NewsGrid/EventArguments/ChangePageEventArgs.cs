using System;
using System.Web.UI;

namespace DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments
{
    public class ChangePageEventArgs : EventArgs
    {
        public int Page { get; set; }

        public bool IsAdminUser { get; set; }

        public StateBag ViewState { get; set; }
    }
}