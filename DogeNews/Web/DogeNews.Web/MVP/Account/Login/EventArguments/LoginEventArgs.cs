namespace DogeNews.Web.MVP.Account.Login.EventArguments
{
    public class LoginEventArgs
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}