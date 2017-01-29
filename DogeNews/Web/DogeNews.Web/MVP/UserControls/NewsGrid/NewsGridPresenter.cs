using System;

using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.Services.Contracts;
using DogeNews.Web.MVP.UserControls.NewsGrid.EventArguments;
using DogeNews.Common.Enums;

using WebFormsMvp;

namespace DogeNews.Web.MVP.UserControls.NewsGrid
{
    public class NewsGridPresenter : Presenter<INewsGridView>
    {
        private const int PageSize = 6;

        private readonly IDataSourceService<NewsItem, NewsWebModel> newsDataSource;

        public NewsGridPresenter(
            INewsGridView view,
            IDataSourceService<NewsItem, NewsWebModel> newsDataSource)
                : base(view)
        {
            this.newsDataSource = newsDataSource;

            this.View.PageLoad += this.PageLoad;
            this.View.ChangePage += this.ChangePage;
            this.View.OrderByDate += this.OrderByDate;
        }

        private void PageLoad(object sender, PageLoadEventArgs e)
        {
            if (!e.IsPostBack)
            {
                e.ViewState["CurrentPage"] = 1;
            }
            
            this.View.Model.CurrentPageNews = this.newsDataSource.GetPageItems(1, PageSize);
            this.View.Model.NewsCount = this.newsDataSource.Count;
            this.View.Model.PageSize = PageSize;
        }

        private void ChangePage(object sender, ChangePageEventArgs e)
        {
            e.ViewState["CurrentPage"] = e.Page;
            this.View.Model.CurrentPageNews = this.newsDataSource.GetPageItems(e.Page, PageSize);
        }

        private void OrderByDate(object sender, OrderByEventArgs e)
        {
            if (e.OrderBy == OrderByType.Ascending)
            {
                this.View.Model.CurrentPageNews =
                    this.newsDataSource.OrderByDateAscending((int)e.ViewState["CurrentPage"], PageSize);
                return;
            }

            this.View.Model.CurrentPageNews =
                this.newsDataSource.OrderByDateDescending((int)e.ViewState["CurrentPage"], PageSize);
        }
    }
}