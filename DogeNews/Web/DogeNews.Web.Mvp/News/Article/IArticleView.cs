using System;
using DogeNews.Web.Mvp.News.Article.EventArguments;
using DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments;
using WebFormsMvp;

namespace DogeNews.Web.Mvp.News.Article
{
    public interface IArticleView : IView<ArticleViewModel>
    {
        event EventHandler<ArticlePageLoadEventArgs> PageLoad;
        event EventHandler<OnArticleDeleteEventArgs> ArticleDelete;
        event EventHandler<OnArticleEditEventArgs> ArticleEdit;
        event EventHandler<OnArticleRestoreEventArgs> ArticleRestore;
    }
}