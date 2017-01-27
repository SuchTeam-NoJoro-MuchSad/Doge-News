using System.Web;

using DogeNews.Web.Managers;
using DogeNews.Web.MVP.Account.Login.EventArguments;
using DogeNews.Web.Helpers;

using Microsoft.AspNet.Identity.Owin;
using WebFormsMvp;

namespace DogeNews.Web.MVP.Account.Login
{
    public class LoginPresenter : Presenter<ILoginView>
    {
        public LoginPresenter(ILoginView view)
            : base(view)
        {
            this.View.LoginUser += this.Login;
        }

        private void Login(object sender, LoginEventArgs e)
        {
            // Validate the user password
            var manager = this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = this.HttpContext.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            // This doen't count login failures towards account lockout
            // To enable password failures to trigger lockout, change to shouldLockout: true
            var result = signinManager
                .PasswordSignIn(e.Email, e.Password, e.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    IdentityHelper.RedirectToReturnUrl(
                        this.Request.QueryString["ReturnUrl"], 
                        this.HttpContext.Response);
                    break;
                case SignInStatus.LockedOut:
                    this.Response.Redirect("/Account/Lockout");
                    break;
                case SignInStatus.RequiresVerification:
                    this.Response.Redirect(
                        string.Format(
                            "/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                            Request.QueryString["ReturnUrl"],
                            e.RememberMe),
                            true);
                    break;
                case SignInStatus.Failure:
                default:
                    this.View.Model.FailureText = "Invalid login attempt";
                    this.View.Model.IsErrorMessageVisible = true;
                    break;
            }
        }
    }
}