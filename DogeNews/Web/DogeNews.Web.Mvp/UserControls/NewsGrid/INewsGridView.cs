using System;

using DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.UserControls.NewsGrid
{
    public interface INewsGridView : IView<NewsGridViewModel>
    {
        event EventHandler<PageLoadEventArgs> PageLoad;
        event EventHandler<ChangePageEventArgs> ChangePage;
        event EventHandler<OrderByEventArgs> OrderByDate;
    }
}