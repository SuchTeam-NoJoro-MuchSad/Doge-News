using System;
using DogeNews.Web.Auth.Models;
using DogeNews.Web.Auth.Presenters;
using DogeNews.Web.Auth.Views;
using DogeNews.Web.Auth.Views.EventArguments;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace DogeNews.Web.Auth
{
    [PresenterBinding(typeof(LoginPresentor))]
    public partial class Login : MvpPage<LoginPageModel>, ILoginView
    {
        public event EventHandler<LoginEventArgs> LoginUser;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LoginSubmitButton_OnClickSubmitButton_Click(object sender, EventArgs e)
        {
            var eventArgs = new LoginEventArgs
            {
                Username = this.Username.Value,
                Password = this.PasswordInput.Value
            };
            
            this.LoginUser(this, eventArgs);
        }
    }
}