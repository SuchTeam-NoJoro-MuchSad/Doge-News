using DogeNews.Web.Auth.Views;
using DogeNews.Web.Auth.Views.EventArguments;
using DogeNews.Web.Services.Contracts;

using WebFormsMvp;

namespace DogeNews.Web.Auth.Presenters
{
    public class RegisterPresenter : Presenter<IRegisterView>
    {
        private readonly IAuthService authService;

        public RegisterPresenter(IRegisterView view, IAuthService authService) 
            : base(view)
        {
            this.authService = authService;
            this.View.RegisterUser += this.RegisterUser;
        }

        private void RegisterUser(object sender, RegisterEventArgs eventArgs)
        {
            bool isUserRegistered = this.authService.RegisterUser(eventArgs.Data);
            if (isUserRegistered)
            {
                this.HttpContext.Response.Redirect("/Auth/Login");
            }
        }
    }
}