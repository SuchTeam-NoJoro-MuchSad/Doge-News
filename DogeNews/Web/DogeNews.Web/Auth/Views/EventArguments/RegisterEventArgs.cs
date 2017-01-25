using System;

using DogeNews.Web.Models;

namespace DogeNews.Web.Auth.Views.EventArguments
{
    public class RegisterEventArgs : EventArgs
    {
        public UserWebModel Data { get; set; }
    }
}