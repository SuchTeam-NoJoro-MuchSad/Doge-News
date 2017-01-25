using System;

using DogeNews.Web.User.Settings.Models;
using DogeNews.Web.User.Settings.Views;
using DogeNews.Web.User.Settings.Presenters;
using DogeNews.Web.User.Settings.Views.EventArguments;

using WebFormsMvp.Web;
using WebFormsMvp;

namespace DogeNews.Web.User.Settings
{
    [PresenterBinding(typeof(SettingsPresenter))]
    public partial class Settings : MvpPage<SettingsPageModel>, ISettingsView
    {
        public event EventHandler<ChangePasswordEventArgs> ChangePassword;

        protected void Page_Load(object sender, EventArgs e)
        {
            string urlUsername = this.Page.RouteData.Values["username"].ToString();
            if (this.Session["Username"] != null && urlUsername != this.Session["Username"].ToString())
            {
                this.Response.Redirect("/");
            }
        }

        protected void ChangePasswordEvent(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var eventArgs = new ChangePasswordEventArgs
                {
                    NewPassword = this.Server.HtmlEncode(this.NewPassword.Text)
                };

                this.ChangePassword(this, eventArgs);
            }
        }
    }
}