using System;
using System.Web.UI;

using DogeNews.Common.Enums;

namespace DogeNews.Web.MVP.UserControls.NewsGrid.EventArguments
{
    public class OrderByEventArgs : EventArgs
    {
        public OrderByType OrderBy { get; set; }

        public StateBag ViewState { get; set; }
    }
}