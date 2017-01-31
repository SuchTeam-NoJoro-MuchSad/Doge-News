using System.Web;
using DogeNews.Web.MVP.News.Article.EventArguments;
using DogeNews.Web.Services.Contracts;
using WebFormsMvp;

namespace DogeNews.Web.MVP.News.Article
{
    public class ArticlePresenter : Presenter<IArticleView>
    {
        private readonly INewsService newsDataSource;


        public ArticlePresenter(IArticleView view,
            INewsService dataSourceService)
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
            var model = this.newsDataSource.GetItemByTitle(title);

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