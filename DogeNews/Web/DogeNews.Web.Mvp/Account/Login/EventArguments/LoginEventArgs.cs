namespace DogeNews.Web.Mvp.Account.Login.EventArguments
{
    public class LoginEventArgs
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
