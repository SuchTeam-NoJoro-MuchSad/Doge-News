using DogeNews.Web.Services.Contracts;
using DogeNews.Web.User.Settings.Views;
using DogeNews.Web.User.Settings.Views.EventArguments;

using WebFormsMvp;

namespace DogeNews.Web.User.Settings.Presenters
{
    public class SettingsPresenter : Presenter<ISettingsView>
    {
        private readonly IUserService userService;

        public SettingsPresenter(ISettingsView view, IUserService userService)
            : base(view)
        {
            this.userService = userService;

            this.View.ChangePassword += this.ChangePassword;
        }

        private void ChangePassword(object sender, ChangePasswordEventArgs e)
        {
            this.userService.ChangePassword(this.HttpContext.Session["Username"].ToString(), e.NewPassword);
            this.View.Model.Message = "Success";
            this.View.Model.IsMessageVisible = true;
        }
    }
}