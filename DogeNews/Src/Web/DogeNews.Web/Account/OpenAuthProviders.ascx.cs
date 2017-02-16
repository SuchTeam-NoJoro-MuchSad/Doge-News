using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

using DogeNews.Web.Identity.Helpers;

namespace DogeNews.Web.Account
{
    public partial class OpenAuthProviders : System.Web.UI.UserControl
    {
        public string ReturnUrl { get; set; }

        public IEnumerable<string> GetProviderNames()
        {
            return Context.GetOwinContext().Authentication.GetExternalAuthenticationTypes().Select(t => t.AuthenticationType);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                string provider = Request.Form["provider"];
                if (provider == null)
                {
                    return;
                }
                // Request a redirect to the external login provider
                string redirectUrl = ResolveUrl(string.Format(
                    CultureInfo.InvariantCulture, 
                    "~/Account/RegisterExternalLogin?{0}={1}&returnUrl={2}", 
                    IdentityHelper.ProviderNameKey, 
                    provider, 
                    this.ReturnUrl));
                AuthenticationProperties properties = new AuthenticationProperties() { RedirectUri = redirectUrl };
                // Add xsrf verification when linking accounts
                if (Context.User.Identity.IsAuthenticated)
                {
                    properties.Dictionary[IdentityHelper.XsrfKey] = Context.User.Identity.GetUserId();
                }

                this.Context
                    .GetOwinContext()
                    .Authentication
                    .Challenge(properties, provider);
                this.Response.StatusCode = 401;
                this.Response.End();
            }
        }
    }
}