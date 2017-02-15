using System;
using System.Web.UI;

using DogeNews.Common.Enums;

namespace DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments
{
    public class OrderByEventArgs : EventArgs
    {
        public bool IsAdminUser { get; set; }

        public OrderByType OrderBy { get; set; }

        public StateBag ViewState { get; set; }
    }
}