using System;

namespace DogeNews.Web.MVP.Account.Register.EventArguments
{
    public class CreateUserEventArgs : EventArgs
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}