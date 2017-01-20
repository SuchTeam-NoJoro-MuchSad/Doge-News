using System;

using DogeNews.Web.Models;

namespace DogeNews.Web.Auth.Views.EventArguments
{
    public class LoginEventArgs : EventArgs
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}