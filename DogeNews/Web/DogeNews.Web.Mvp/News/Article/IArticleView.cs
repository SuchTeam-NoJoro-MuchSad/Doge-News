using System;
using DogeNews.Web.Mvp.News.Article.EventArguments;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.News.Article
{
    public interface IArticleView : IView<ArticleViewModel>
    {
        event EventHandler<ArticlePageLoadEventArgs> PageLoad;
    }
}