using System;

using DogeNews.Web.Mvp.Account.Register;
using DogeNews.Web.Mvp.Account.Register.EventArguments;

using WebFormsMvp.Web;
using WebFormsMvp;

namespace DogeNews.Web.Account
{
    [PresenterBinding(typeof(RegisterPresenter))]
    public partial class Register : MvpPage<RegisterViewModel>, IRegisterView
    {
        public event EventHandler<CreateUserEventArgs> CreateUser;

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var args = new CreateUserEventArgs
            {
                UserName = this.Server.HtmlEncode(this.Username.Text),
                Password = this.Server.HtmlEncode(this.Password.Text)
            };

            this.CreateUser(this, args);
        }
    }
}