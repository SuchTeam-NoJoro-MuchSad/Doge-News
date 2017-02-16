using System;
using System.Web;

using DogeNews.Web.Mvp.Account.Login.EventArguments;
using DogeNews.Web.Mvp.Account.Login;

using WebFormsMvp.Web;
using WebFormsMvp;

namespace DogeNews.Web.Account
{
    [PresenterBinding(typeof(LoginPresenter))]
    public partial class Login : MvpPage<LoginViewModel>, ILoginView
    {
        public event EventHandler<LoginEventArgs> LoginUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RegisterHyperLink.NavigateUrl = "Register";
            this.OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

            string returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);

            if (!string.IsNullOrEmpty(returnUrl))
            {
                this.RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }

            this.UserNameLabel.Attributes.Add("data-error","errrr");
            this.UserNameLabel.Attributes.Add("data-success", "succ");
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                LoginEventArgs args = new LoginEventArgs
                {
                    UserName = this.Server.HtmlEncode(this.UserName.Text),
                    Password = this.Server.HtmlEncode(this.Password.Text),
                    RememberMe = this.RememberMe.Checked
                };

                this.LoginUser(this, args);
            }
        }
    }
}