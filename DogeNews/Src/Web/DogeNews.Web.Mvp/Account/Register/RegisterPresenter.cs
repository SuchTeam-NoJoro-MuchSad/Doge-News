using System.Linq;
using System.Web;

using DogeNews.Common.Constants;
using DogeNews.Data.Models;
using DogeNews.Web.Identity.Helpers;
using DogeNews.Web.Identity.Managers;
using DogeNews.Web.Mvp.Account.Register.EventArguments;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.Account.Register
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
            ApplicationUserManager manager = this.HttpContext
                .GetOwinContext()
                .GetUserManager<ApplicationUserManager>();
            ApplicationSignInManager signInManager = this.HttpContext
                .GetOwinContext()
                .Get<ApplicationSignInManager>();
            User user = new User { UserName = e.UserName };
            IdentityResult result = manager.Create(user, e.Password);

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
