using System.Collections.Generic;
using System.Web.Configuration;

using DogeNews.Web.Auth.Views;
using DogeNews.Web.Auth.Views.EventArguments;
using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Models;

using WebFormsMvp;

namespace DogeNews.Web.Auth.Presenters
{
    public class LoginPresenter : Presenter<ILoginView>
    {
        private const int CookieLifeTimeInDays = 1;

        private readonly IAuthService authService;
        private readonly ICookieProvider cookieProvider;
        private readonly IEncryptionProvider encryptionProvider;
        private readonly IAppConfigurationProvider configProvider;

        public LoginPresenter(
            ILoginView view,
            IAuthService authService,
            ICookieProvider cookieProvider,
            IEncryptionProvider encryptionProvider,
            IAppConfigurationProvider configProvider)
            : base(view)
        {
            this.authService = authService;
            this.cookieProvider = cookieProvider;
            this.encryptionProvider = encryptionProvider;
            this.configProvider = configProvider;
            this.View.LoginUser += this.LoginUser;
        }

        private void LoginUser(object sender, LoginEventArgs eventArgs)
        {
            var user = this.authService.LoginUser(eventArgs.Username, eventArgs.Password);
            if (user == null)
            {
                // error
                return;
            }

            var values = this.GetCookieValuesToSet(user);
            var cookie = this.cookieProvider
                .GetAuthenticationCookie(this.configProvider.AuthCookieName, CookieLifeTimeInDays, values);

            this.Response.Cookies.Add(cookie);
            this.HttpContext.Session["Username"] = user.Username;
            this.HttpContext.Session["Id"] = user.Id;
            this.HttpContext.Response.Redirect("/");
        }

        private IEnumerable<KeyValuePair<string, string>> GetCookieValuesToSet(UserWebModel user)
        {
            string encryptionKey = WebConfigurationManager.AppSettings["EncryptionKey"];
            string idKey = this.encryptionProvider.Encrypt("Id", encryptionKey);
            string idValue = this.encryptionProvider.Encrypt(user.Id.ToString(), encryptionKey);
            string usernameKey = this.encryptionProvider.Encrypt("Username", encryptionKey);
            string usernameValue = this.encryptionProvider.Encrypt(user.Username, encryptionKey);
            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(idKey, idValue),
                new KeyValuePair<string, string>(usernameKey, usernameValue)
            };

            return values;
        }
    }
}