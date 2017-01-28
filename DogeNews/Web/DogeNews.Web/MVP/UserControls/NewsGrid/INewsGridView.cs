using System;

using DogeNews.Web.MVP.UserControls.NewsGrid.EventArguments;

using WebFormsMvp;

namespace DogeNews.Web.MVP.UserControls.NewsGrid
{
    public interface INewsGridView : IView<NewsGridViewModel>
    {
        event EventHandler PageLoad;
        event EventHandler<ChangePageEventArgs> ChangePage;
    }
}