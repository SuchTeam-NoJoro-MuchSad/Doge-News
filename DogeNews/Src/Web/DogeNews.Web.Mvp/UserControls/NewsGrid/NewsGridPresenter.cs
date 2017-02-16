using DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments;
using DogeNews.Common.Enums;
using DogeNews.Data.Models;
using DogeNews.Web.DataSources.Contracts;
using DogeNews.Web.Models;
using DogeNews.Web.Services.Contracts;
using DogeNews.Services.Http.Contracts;

using WebFormsMvp;
using DogeNews.Common.Validators;

namespace DogeNews.Web.Mvp.UserControls.NewsGrid
{
    public class NewsGridPresenter : Presenter<INewsGridView>
    {
        private const int PageSize = 6;
        private const string NewsCategoryQueryStringKey = "name";

        private INewsDataSource<NewsItem, NewsWebModel> newsDataSource;
        private IHttpUtilityService httpUtilityService;
        private IArticleManagementService articleManagementService;

        private string newsCategory;

        public NewsGridPresenter(INewsGridView view,
            INewsDataSource<NewsItem, NewsWebModel> newsDataSource,
            IHttpUtilityService httpUtilityService,
            IArticleManagementService articleManagementService)
                : base(view)
        {
            Validator.ValidateThatObjectIsNotNull(newsDataSource, nameof(newsDataSource));
            Validator.ValidateThatObjectIsNotNull(httpUtilityService, nameof(httpUtilityService));
            Validator.ValidateThatObjectIsNotNull(articleManagementService, nameof(articleManagementService));

            this.newsDataSource = newsDataSource;
            this.httpUtilityService = httpUtilityService;
            this.articleManagementService = articleManagementService;

            this.View.PageLoad += this.PageLoad;
            this.View.ChangePage += this.ChangePage;
            this.View.OrderByDate += this.OrderByDate;
            this.View.ArticleDelete += this.ArticleDelete;
            this.View.ArticleEdit += this.ArticleEdit;
            this.View.ArticleRestore += this.ArticleRestore;
        }

        private void ArticleRestore(object sender, OnArticleRestoreEventArgs e)
        {
            Validator.ValidateThatObjectIsNotNull(e, nameof(e));

            int id = int.Parse(e.NewsItemId);
            this.articleManagementService.Restore(id);
        }

        private void ArticleEdit(object sender, OnArticleEditEventArgs e)
        {
            Validator.ValidateThatObjectIsNotNull(e, nameof(e));

            if (e.IsAdminUser)
            {
                this.HttpContext.Response.Redirect($"~/News/Edit?id={e.NewsItemId}");
            }
        }

        private void ArticleDelete(object sender, OnArticleDeleteEventArgs e)
        {
            Validator.ValidateThatObjectIsNotNull(e, nameof(e));

            int id = int.Parse(e.NewsItemId);
            this.articleManagementService.Delete(id);
        }

        public void PageLoad(object sender, PageLoadEventArgs e)
        {
            Validator.ValidateThatObjectIsNotNull(e, nameof(e));

            if (!e.IsPostBack)
            {
                e.ViewState["CurrentPage"] = 1;
            }

            if (e.QueryString != null)
            {
                var parsedQueryString = this.httpUtilityService.ParseQueryString(e.QueryString);
                this.newsCategory = parsedQueryString[NewsCategoryQueryStringKey];
            }

            this.View.Model.CurrentPageNews = this.newsDataSource.GetPageItems(1, PageSize, e.IsAdminUser, this.newsCategory);
            this.View.Model.NewsCount = this.newsDataSource.Count;
            this.View.Model.PageSize = PageSize;
        }

        public void ChangePage(object sender, ChangePageEventArgs e)
        {
            Validator.ValidateThatObjectIsNotNull(e, nameof(e));

            e.ViewState["CurrentPage"] = e.Page;
            this.View.Model.CurrentPageNews = this.newsDataSource.GetPageItems(e.Page, PageSize, e.IsAdminUser, this.newsCategory);
        }

        public void OrderByDate(object sender, OrderByEventArgs e)
        {
            Validator.ValidateThatObjectIsNotNull(e, nameof(e));

            if (e.OrderBy == OrderByType.Ascending)
            {
                this.View.Model.CurrentPageNews = this.newsDataSource.OrderByAscending(
                    x => x.CreatedOn,
                    (int)e.ViewState["CurrentPage"],
                    PageSize,
                    e.IsAdminUser,
                    this.newsCategory);
            }
            else
            {
                this.View.Model.CurrentPageNews = this.newsDataSource.OrderByDescending(
                x => x.CreatedOn,
                (int)e.ViewState["CurrentPage"],
                PageSize,
                e.IsAdminUser,
                this.newsCategory);
            }
        }
    }
}