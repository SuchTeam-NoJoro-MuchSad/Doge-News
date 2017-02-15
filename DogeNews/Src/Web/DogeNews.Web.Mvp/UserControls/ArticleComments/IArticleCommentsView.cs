using System;

using DogeNews.Web.Mvp.UserControls.ArticleComments.EventArguments;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.UserControls.ArticleComments
{
    public interface IArticleCommentsView : IView<ArticleCommentsViewModel>
    {
        event EventHandler<ArticleCommetnsPageLoadEventArgs> PageLoad;
        event EventHandler<AddCommentEventArguments> AddComment;
    }
}