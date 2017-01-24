using DogeNews.Web.User.Settings.Views;
using WebFormsMvp;

namespace DogeNews.Web.User.Settings.Presenters
{
    public class SettingsPresenter : Presenter<ISettingsView>
    {
        public SettingsPresenter(ISettingsView view) 
            : base(view)
        {
        }
    }
}