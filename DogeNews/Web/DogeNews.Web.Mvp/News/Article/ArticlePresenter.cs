using DogeNews.Web.Mvp.News.Article.EventArguments;
using DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments;
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
        private IArticleManagementService articleManagementService;

        public ArticlePresenter(
            IArticleView view,
            INewsService newsService,
            IHttpUtilityService httpUtilityService,
            IHttpResponseService httpResponseService,
            IArticleManagementService articleManagementService) : base(view)
        {
            this.newsService = newsService;
            this.httpUtilityService = httpUtilityService;
            this.httpResponseService = httpResponseService;
            this.articleManagementService = articleManagementService;

            this.View.PageLoad += this.PageLoad;
            this.View.ArticleDelete += this.ArticleDelete;
            this.View.ArticleEdit += this.ArticleEdit;
            this.View.ArticleRestore += this.ArticleRestore;
        }

        private void ArticleRestore(object sender, OnArticleRestoreEventArgs e)
        {
            this.articleManagementService.Restore(e.NewsItemId);
            this.View.Model.NewsModel = this.newsService.GetItemById(e.NewsItemId);
        }

        private void ArticleEdit(object sender, OnArticleEditEventArgs e)
        {
            if (e.IsAdminUser)
            {
                this.HttpContext.Response.Redirect($"~/News/Edit?id={e.NewsItemId}");
            }
        }

        private void ArticleDelete(object sender, OnArticleDeleteEventArgs e)
        {
            this.articleManagementService.Delete(e.NewsItemId);
            this.View.Model.NewsModel = this.newsService.GetItemById(e.NewsItemId);
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