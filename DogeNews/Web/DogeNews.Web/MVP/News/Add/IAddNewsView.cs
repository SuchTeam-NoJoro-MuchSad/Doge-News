using System;

using DogeNews.Web.MVP.News.Add.EventArguments;

using WebFormsMvp;

namespace DogeNews.Web.MVP.News.Add
{
    public interface IAddNewsView : IView<AddNewsViewModel>
    {
        event EventHandler<AddNewsEventArgs> AddNews;
    }
}