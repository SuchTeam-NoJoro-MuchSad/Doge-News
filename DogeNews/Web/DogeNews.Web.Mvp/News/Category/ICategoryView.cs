using System;
using DogeNews.Web.Mvp.News.Category.EventArguments;
using WebFormsMvp;

namespace DogeNews.Web.Mvp.News.Category
{
    public interface ICategoryView : IView<CategoryViewModel>
    {
        event EventHandler<CategoryPageLoadEventArgs> PageLoad;
    }
}