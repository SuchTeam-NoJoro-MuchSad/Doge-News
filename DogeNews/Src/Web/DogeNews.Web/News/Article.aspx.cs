using System;
using System.Web.UI.WebControls;

using DogeNews.Web.Mvp.News.Article;
using DogeNews.Web.Mvp.News.Article.EventArguments;
using DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments;

using WebFormsMvp;
using WebFormsMvp.Web;

namespace DogeNews.Web.News
{
    [PresenterBinding(typeof(ArticlePresenter))]
    public partial class Article : MvpPage<ArticleViewModel>, IArticleView
    {
        public event EventHandler<ArticlePageLoadEventArgs> PageLoad;
        public event EventHandler<OnArticleDeleteEventArgs> ArticleDelete;
        public event EventHandler<OnArticleEditEventArgs> ArticleEdit;
        public event EventHandler<OnArticleRestoreEventArgs> ArticleRestore;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                this.PageLoad(this, new ArticlePageLoadEventArgs { IsPostBack = true, ViewState = this.ViewState, QueryString = this.ClientQueryString });
                return;
            }

            this.PageLoad(this, new ArticlePageLoadEventArgs { IsPostBack = false, ViewState = this.ViewState, QueryString = this.ClientQueryString });
        }

        protected void ArticleDeleteButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string newsItemId = button.CommandArgument;

            OnArticleDeleteEventArgs eventArgs = new OnArticleDeleteEventArgs
            {
                NewsItemId = newsItemId
            };

            this.ArticleDelete(this, eventArgs);
        }

        protected void ArticleEditButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string newsItemId = button.CommandArgument;

            OnArticleEditEventArgs eventArgs = new OnArticleEditEventArgs
            {
                IsAdminUser = this.Context.User.IsInRole(Common.Constants.Roles.Admin),
                NewsItemId = newsItemId
            };

            this.ArticleEdit(this, eventArgs);
        }

        protected void ArticleRestoreButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string newsItemId = button.CommandArgument;

            OnArticleRestoreEventArgs eventArgs = new OnArticleRestoreEventArgs
            {
                NewsItemId = newsItemId
            };

            this.ArticleRestore(this, eventArgs);
        }
    }
}