using System;
using DogeNews.Web.MVP.News.Article.EventArguments;

using WebFormsMvp;

namespace DogeNews.Web.MVP.News.Article
{
    public interface IArticleView : IView<ArticleViewModel>
    {
        event EventHandler<ArticlePageLoadEventArgs> PageLoad;
    }
}