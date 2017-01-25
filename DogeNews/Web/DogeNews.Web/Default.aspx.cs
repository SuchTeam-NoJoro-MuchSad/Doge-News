using WebFormsMvp.Web;
using WebFormsMvp;

using DogeNews.Web.MVP.Default;

namespace DogeNews.Web
{
    [PresenterBinding(typeof(DefaultPresenter))]
    public partial class _Default : MvpPage<DefaultViewModel>, IDefaultView
    {
    }
}