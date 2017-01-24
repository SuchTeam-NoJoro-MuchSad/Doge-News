using System;

using DogeNews.Web.User.Settings.Models;
using DogeNews.Web.User.Settings.Views;
using DogeNews.Web.User.Settings.Presenters;

using WebFormsMvp.Web;
using WebFormsMvp;

namespace DogeNews.Web.User.Settings
{
    [PresenterBinding(typeof(SettingsPresenter))]
    public partial class Settings : MvpPage<SettingsPageModel>, ISettingsView
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string urlUsername = this.Page.RouteData.Values["username"].ToString();
            if (urlUsername != this.Session["Username"].ToString())
            {
                this.Response.Redirect("/");
            }
        }
    }
}