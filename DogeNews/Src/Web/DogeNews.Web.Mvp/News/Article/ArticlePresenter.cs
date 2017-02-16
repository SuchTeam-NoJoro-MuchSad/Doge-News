using DogeNews.Common.Validators;
using DogeNews.Services.Http.Contracts;
using DogeNews.Web.Mvp.News.Article.EventArguments;
using DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments;
using DogeNews.Web.Services.Contracts;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.News.Article
{
    public class ArticlePresenter : Presenter<IArticleView>
    {
        private readonly INewsService newsService;
        private readonly IHttpUtilityService httpUtilityService;
        private readonly IHttpResponseService httpResponseService;
        private readonly IArticleManagementService articleManagementService;

        public ArticlePresenter(
            IArticleView view,
            INewsService newsService,
            IHttpUtilityService httpUtilityService,
            IHttpResponseService httpResponseService,
            IArticleManagementService articleManagementService) : base(view)
        {
            Validator.ValidateThatObjectIsNotNull(newsService, nameof(newsService));
            Validator.ValidateThatObjectIsNotNull(httpUtilityService, nameof(httpUtilityService));
            Validator.ValidateThatObjectIsNotNull(httpResponseService, nameof(httpResponseService));
            Validator.ValidateThatObjectIsNotNull(articleManagementService, nameof(articleManagementService));

            this.newsService = newsService;
            this.httpUtilityService = httpUtilityService;
            this.httpResponseService = httpResponseService;
            this.articleManagementService = articleManagementService;

            this.View.PageLoad += this.PageLoad;
            this.View.ArticleDelete += this.ArticleDelete;
            this.View.ArticleEdit += this.ArticleEdit;
            this.View.ArticleRestore += this.ArticleRestore;
        }

        public void ArticleRestore(object sender, OnArticleRestoreEventArgs e)
        {
            Validator.ValidateThatObjectIsNotNull(e, nameof(e));

            int id = int.Parse(e.NewsItemId);

            this.articleManagementService.Restore(id);
            this.View.Model.NewsModel = this.newsService.GetItemById(id);
        }

        public void ArticleEdit(object sender, OnArticleEditEventArgs e)
        {
            Validator.ValidateThatObjectIsNotNull(e, nameof(e));

            if (e.IsAdminUser)
            {
                this.httpResponseService.Redirect($"~/News/Edit?id={e.NewsItemId}");
            }
        }

        public void ArticleDelete(object sender, OnArticleDeleteEventArgs e)
        {
            Validator.ValidateThatObjectIsNotNull(e, nameof(e));

            int id = int.Parse(e.NewsItemId);

            this.articleManagementService.Delete(id);
            this.View.Model.NewsModel = this.newsService.GetItemById(id);
        }

        public void PageLoad(object sender, ArticlePageLoadEventArgs e)
        {
            Validator.ValidateThatObjectIsNotNull(e, nameof(e));
            
            var parsedQueryString = this.httpUtilityService.ParseQueryString(e.QueryString);

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