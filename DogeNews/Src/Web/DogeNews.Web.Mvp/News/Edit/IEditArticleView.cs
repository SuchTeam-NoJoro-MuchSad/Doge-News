using System;
using DogeNews.Web.Mvp.News.Edit.EventArguments;
using WebFormsMvp;

namespace DogeNews.Web.Mvp.News.Edit
{
    public interface IEditArticleView : IView<EditArticleViewModel>
    {
        event EventHandler<PreInitPageEventArgs> PreInitPageEvent;

        event EventHandler<EditArticleEventArgs> EditArticleButtonClick;
    }
}