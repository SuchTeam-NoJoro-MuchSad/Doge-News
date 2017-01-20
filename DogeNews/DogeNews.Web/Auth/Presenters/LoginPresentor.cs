using System;
using System.Web;

using DogeNews.Web.Auth.Views;
using DogeNews.Web.Auth.Views.EventArguments;
using DogeNews.Web.Services.Contracts;

using Newtonsoft.Json;
using WebFormsMvp;

namespace DogeNews.Web.Auth.Presenters
{
    public class LoginPresentor : Presenter<ILoginView>
    {
        private readonly IAuthService authService;

        public LoginPresentor(ILoginView view, IAuthService authService)
            : base(view)
        {
            this.authService = authService;
            this.View.LoginUser += this.LoginUser;
        }

        private void LoginUser(object sender, LoginEventArgs eventArgs)
        {
            var user = this.authService.LoginUser(eventArgs.Username, eventArgs.Password);
            if (user == null)
            {
                // error 
            }
            else
            {
                var cookie = new HttpCookie("UserCookie");
                cookie["username"] = user.Username;
                cookie["firstname"] = user.FirstName;
                cookie["lastname"] = user.LastName;
                cookie["id"] = user.Id.ToString();
                cookie.Expires = DateTime.UtcNow.AddDays(1d);
                this.Response.Cookies.Add(cookie);
                this.HttpContext.Session["username"] = user.Username;
                this.HttpContext.Response.Redirect("/");
            }
        }
    }
}