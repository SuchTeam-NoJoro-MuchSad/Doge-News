using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments;
using DogeNews.Common.Enums;
using DogeNews.Data.Models;
using DogeNews.Web.DataSources.Contracts;
using DogeNews.Web.Models;
using WebFormsMvp;

namespace DogeNews.Web.Mvp.UserControls.NewsGrid
{
    public class NewsGridPresenter : Presenter<INewsGridView>
    {
        private INewsDataSource<NewsItem, NewsWebModel> newsDataSource;
        private const int PageSize = 6;
        private string newsCategory;


        public NewsGridPresenter(INewsGridView view, INewsDataSource<NewsItem, NewsWebModel> newsDataSource)
                : base(view)
        {
            this.newsDataSource = newsDataSource;

            this.View.PageLoad += this.PageLoad;
            this.View.ChangePage += this.ChangePage;
            this.View.OrderByDate += this.OrderByDate;
        }

        public void PageLoad(object sender, PageLoadEventArgs e)
        {
            if (!e.IsPostBack)
            {
                e.ViewState["CurrentPage"] = 1;
            }

            this.newsCategory = e.QueryString;

            this.View.Model.CurrentPageNews = this.newsDataSource.GetPageItems(1, PageSize, this.newsCategory);
            this.View.Model.NewsCount = this.newsDataSource.Count;
            this.View.Model.PageSize = PageSize;
        }

        public void ChangePage(object sender, ChangePageEventArgs e)
        {
            e.ViewState["CurrentPage"] = e.Page;
            this.View.Model.CurrentPageNews = this.newsDataSource.GetPageItems(e.Page, PageSize, this.newsCategory);
        }

        public void OrderByDate(object sender, OrderByEventArgs e)
        {
            if (e.OrderBy == OrderByType.Ascending)
            {
                this.View.Model.CurrentPageNews = this.newsDataSource.OrderByAscending(
                    x => x.CreatedOn,
                    (int)e.ViewState["CurrentPage"],
                    PageSize,
                    this.newsCategory);
                return;
            }

            this.View.Model.CurrentPageNews = this.newsDataSource.OrderByDescending(
                x => x.CreatedOn,
                (int)e.ViewState["CurrentPage"],
                PageSize,
                this.newsCategory);
        }

        
    }
}