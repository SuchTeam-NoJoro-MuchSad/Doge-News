using System;

namespace DogeNews.Web.User.Settings.Views.EventArguments
{
    public class ChangePasswordEventArgs : EventArgs
    {
        public string NewPassword { get; set; }
    }
}