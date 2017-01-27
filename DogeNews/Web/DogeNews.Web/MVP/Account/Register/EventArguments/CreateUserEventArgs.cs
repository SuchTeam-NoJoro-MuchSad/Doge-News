using System;

namespace DogeNews.Web.MVP.Account.Register.EventArguments
{
    public class CreateUserEventArgs : EventArgs
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}