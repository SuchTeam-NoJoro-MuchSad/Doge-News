using System;
using System.Web;

using DogeNews.Web.Common.Enums;

namespace DogeNews.Web.User.Admin.Views.EventArguments
{
    public class AdminAddNewsEventArgs : EventArgs
    {
        public string Title { get; set; }

        public HttpPostedFile Image { get; set; }

        public string Content { get; set; }

        public NewsCategoryType Category { get; set; }
    }
}