using System;

using DogeNews.Web.User.Settings.Models;
using WebFormsMvp;
using DogeNews.Web.User.Settings.Views.EventArguments;

namespace DogeNews.Web.User.Settings.Views
{
    public interface ISettingsView : IView<SettingsPageModel>
    {
        event EventHandler<ChangePasswordEventArgs> ChangePassword;
    }
}
