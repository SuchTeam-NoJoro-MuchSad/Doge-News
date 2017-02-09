using System.Web;

using DogeNews.Web.Identity.Managers;
using DogeNews.Web.Identity.Helpers;
using DogeNews.Web.Mvp.Account.Login.EventArguments;
using DogeNews.Web.Services.Contracts;
using DogeNews.Common.Enums;

using WebFormsMvp;
using Microsoft.AspNet.Identity.Owin;

namespace DogeNews.Web.Mvp.Account.Login
{
    public class LoginPresenter : Presenter<ILoginView>
    {
        private readonly INotificationsService notificationsService;

        public LoginPresenter(ILoginView view, INotificationsService notificationsService)
            : base(view)
        {
            this.notificationsService = notificationsService;

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
                .PasswordSignIn(e.UserName, e.Password, e.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    int delay = 1000;

                    this.notificationsService
                        .Toast("Login successful.", delay, NotificationType.Success)
                        .Sleep(delay);
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
