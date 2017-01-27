using System;
using System.Web;

using DogeNews.Web.MVP.Account.Login.EventArguments;
using DogeNews.Web.MVP.Account.Login;

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

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);

            if (!string.IsNullOrEmpty(returnUrl))
            {
                this.RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }

            this.EmailLabel.Attributes.Add("data-error","errrr");
            this.EmailLabel.Attributes.Add("data-success", "succ");
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                var args = new LoginEventArgs
                {
                    Email = this.Email.Text,
                    Password = this.Password.Text,
                    RememberMe = this.RememberMe.Checked
                };

                this.LoginUser(this, args);
            }
        }
    }
}