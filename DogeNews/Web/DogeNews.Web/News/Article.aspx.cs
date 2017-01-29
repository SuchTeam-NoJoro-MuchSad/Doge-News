using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DogeNews.Web.MVP.News.Article;
using DogeNews.Web.MVP.News.Article.EventArguments;
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