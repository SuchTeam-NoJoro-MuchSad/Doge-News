using System;

using DogeNews.Web.Mvp.News.Add.EventArguments;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.News.Add
{
    public interface IAddNewsView : IView<AddNewsViewModel>
    {
        event EventHandler<AddNewsEventArgs> AddNews;
    }
}