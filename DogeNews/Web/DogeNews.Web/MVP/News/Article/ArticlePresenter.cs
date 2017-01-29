using System.Web;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.MVP.News.Article.EventArguments;
using DogeNews.Web.MVP.UserControls.NewsGrid.EventArguments;
using DogeNews.Web.Services.Contracts;
using Microsoft.Ajax.Utilities;
using WebFormsMvp;

namespace DogeNews.Web.MVP.News.Article
{
    public class ArticlePresenter : Presenter<IArticleView>
    {
        private readonly IDataSourceService<NewsItem, NewsWebModel> newsDataSource;

        public ArticlePresenter(IArticleView view,
            IDataSourceService<NewsItem, NewsWebModel> dataSourceService)
            : base(view)
        {
            this.newsDataSource = dataSourceService;

            this.View.PageLoad += this.PageLoad;
        }

        private void PageLoad(object sender, ArticlePageLoadEventArgs eventArgs)
        {
            var parsedQueryString = HttpUtility.ParseQueryString(eventArgs.QueryString);

            if (parsedQueryString.Count <= 0)
            {
                this.Response.Clear();
                this.Response.StatusCode = 404;
                Response.End();
            }

            var title = parsedQueryString["title"];
            var model = this.newsDataSource.GetNewsItemByTitle(title);

            if (model == null)
            {
                this.Response.Clear();
                this.Response.StatusCode = 404;
                Response.End();
            }

            this.View.Model.NewsModel = model;
        }
    }
}