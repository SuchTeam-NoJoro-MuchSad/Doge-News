using System;
using DogeNews.Web.MVP.UserControls.ArticleComments.EventArguments;
using WebFormsMvp;

namespace DogeNews.Web.MVP.UserControls.ArticleComments
{
    public interface IArticleCommentsView : IView<ArticleCommentsViewModel>
    {
        event EventHandler<ArticleCommetnsPageLoadEventArgs> PageLoad;
        event EventHandler<AddCommentEventArguments> AddComment;
    }
}