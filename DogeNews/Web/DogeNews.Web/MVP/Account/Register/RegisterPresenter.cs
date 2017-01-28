using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using WebFormsMvp;

using DogeNews.Web.Helpers;
using DogeNews.Web.Managers;
using DogeNews.Data.Models;
using DogeNews.Web.MVP.Account.Register.EventArguments;
using DogeNews.Common.Constants;

namespace DogeNews.Web.MVP.Account.Register
{
    public class RegisterPresenter : Presenter<IRegisterView>
    {
        public RegisterPresenter(IRegisterView view)
            : base(view)
        {
            this.View.CreateUser += this.CreateUser;
        }

        private void CreateUser(object sender, CreateUserEventArgs e)
        {
            var manager = this.HttpContext
                .GetOwinContext()
                .GetUserManager<ApplicationUserManager>();
            var signInManager = this.HttpContext
                .GetOwinContext()
                .Get<ApplicationSignInManager>();
            var user = new User { UserName = e.UserName };
            var result = manager.Create(user, e.Password);
            
            if (result.Succeeded)
            {
                manager.AddToRole(user.Id, Roles.Normal);
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(this.Request.QueryString["ReturnUrl"], this.HttpContext.Response);
            }
            else
            {
                this.View.Model.ErrorMessage = result.Errors.FirstOrDefault();
            }
        }
    }
}