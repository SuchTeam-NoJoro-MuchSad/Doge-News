using System;

using DogeNews.Web.Mvp.News.Article;
using DogeNews.Web.Mvp.News.Article.EventArguments;

using WebFormsMvp;
using WebFormsMvp.Web;

namespace DogeNews.Web.News
{
    [PresenterBinding(typeof(ArticlePresenter))]
    public partial class Article : MvpPage<ArticleViewModel>, IArticleView
    {
        public event EventHandler<ArticlePageLoadEventArgs> PageLoad;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                this.PageLoad(this, new ArticlePageLoadEventArgs { IsPostBack = true, ViewState = this.ViewState, QueryString = this.ClientQueryString });
                return;
            }

            this.PageLoad(this, new ArticlePageLoadEventArgs { IsPostBack = false, ViewState = this.ViewState, QueryString = this.ClientQueryString });
        }
    }
}