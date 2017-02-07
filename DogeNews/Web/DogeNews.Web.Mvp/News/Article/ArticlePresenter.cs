using DogeNews.Web.Mvp.News.Article.EventArguments;
using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Services.Contracts.Http;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.News.Article
{
    public class ArticlePresenter : Presenter<IArticleView>
    {
        private readonly INewsService newsService;
        private readonly IHttpUtilityService httpUtilityService;
        private readonly IHttpResponseService httpResponseService;

        public ArticlePresenter(
            IArticleView view,
            INewsService newsService,
            IHttpUtilityService httpUtilityService,
            IHttpResponseService httpResponseService) : base(view)
        {
            this.newsService = newsService;
            this.httpUtilityService = httpUtilityService;
            this.httpResponseService = httpResponseService;

            this.View.PageLoad += this.PageLoad;
        }

        public void PageLoad(object sender, ArticlePageLoadEventArgs eventArgs)
        {
            var parsedQueryString = this.httpUtilityService.ParseQueryString(eventArgs.QueryString);

            if (parsedQueryString.Count <= 0)
            {
                this.Send404();
                return;
            }

            var title = parsedQueryString["title"];
            var model = this.newsService.GetItemByTitle(title);

            if (model == null)
            {
                this.Send404();
                return;
            }

            this.View.Model.NewsModel = model;
        }

        private void Send404()
        {
            this.httpResponseService.Clear();
            this.httpResponseService.SetStatusCode(404);
            this.httpResponseService.End();
        }
    }
}