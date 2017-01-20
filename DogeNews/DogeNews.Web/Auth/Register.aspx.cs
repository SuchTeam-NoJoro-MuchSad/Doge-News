using System;

using DogeNews.Web.Auth.Presenters;
using DogeNews.Web.Auth.Models;
using DogeNews.Web.Auth.Views;
using DogeNews.Web.Auth.Views.EventArguments;
using DogeNews.Web.Models;

using WebFormsMvp;
using WebFormsMvp.Web;

namespace DogeNews.Web.Auth
{
    [PresenterBinding(typeof(RegisterPresenter))]
    public partial class Register : MvpPage<RegisterPageModel>, IRegisterView
    {
        public event EventHandler<RegisterEventArgs> RegisterUser;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RegisterSubmitButton_Click(object sender, EventArgs e)
        {
            var userModel = new UserWebModel
            {
                Email = this.Server.HtmlEncode(this.EmailInput.Value),
                FirstName = this.Server.HtmlEncode(this.FirstNameInput.Value),
                LastName = this.Server.HtmlEncode(this.LastNameInput.Value),
                Password = this.Server.HtmlEncode(this.PassWordInput.Value),
                Username = this.Server.HtmlEncode(this.Username.Value)
            };
            var eventArgs = new RegisterEventArgs { Data = userModel };

            this.RegisterUser(this, eventArgs);
        }
    }
}