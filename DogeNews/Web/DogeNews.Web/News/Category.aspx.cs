using System;

using DogeNews.Web.Mvp.News.Category;
using DogeNews.Web.Mvp.News.Category.EventArguments;

using WebFormsMvp;
using WebFormsMvp.Web;

namespace DogeNews.Web.News
{
    [PresenterBinding(typeof(CategoryPresenter))]
    public partial class Category : MvpPage<CategoryViewModel>, ICategoryView
    {
        public event EventHandler<CategoryPageLoadEventArgs> PageLoad;

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}