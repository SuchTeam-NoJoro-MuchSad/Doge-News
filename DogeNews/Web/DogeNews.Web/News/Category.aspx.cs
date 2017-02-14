using DogeNews.Web.Mvp.News.Category;

using WebFormsMvp;
using WebFormsMvp.Web;

namespace DogeNews.Web.News
{
    [PresenterBinding(typeof(CategoryPresenter))]
    public partial class Category : MvpPage<CategoryViewModel>, ICategoryView
    {
    }
}