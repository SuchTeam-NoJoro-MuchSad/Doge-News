using System;

using DogeNews.Web.Models;

namespace DogeNews.Web.Auth.Views.EventArgumentss
{
    public class RegisterEventArgs : EventArgs
    {
        public UserWebModel Data { get; set; }
    }
}