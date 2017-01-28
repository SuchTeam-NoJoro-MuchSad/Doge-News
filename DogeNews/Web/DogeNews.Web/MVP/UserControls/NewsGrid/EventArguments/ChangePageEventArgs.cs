using System;

namespace DogeNews.Web.MVP.UserControls.NewsGrid.EventArguments
{
    public class ChangePageEventArgs : EventArgs
    {
        public int Page { get; set; }
    }
}