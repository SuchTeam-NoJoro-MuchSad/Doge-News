using System;

using DogeNews.Web.MVP.Account.Register;
using DogeNews.Web.MVP.Account.Register.EventArguments;

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
            var args = new CreateUserEventArgs { Email = this.Email.Text, Password = this.Password.Text };
            this.CreateUser(this, args);
        }
    }
}