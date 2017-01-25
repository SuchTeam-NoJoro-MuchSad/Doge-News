using WebFormsMvp;

namespace DogeNews.Web.MVP.Default
{
    public class DefaultPresenter : Presenter<IDefaultView>
    {
        public DefaultPresenter(IDefaultView view) 
            : base(view)
        {
        }
    }
}