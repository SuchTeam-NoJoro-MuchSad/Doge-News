using WebFormsMvp;

namespace DogeNews.Web.Mvp.Default
{
    public class DefaultPresenter : Presenter<IDefaultView>
    {
        public DefaultPresenter(IDefaultView view) 
            : base(view)
        {
        }
    }
}