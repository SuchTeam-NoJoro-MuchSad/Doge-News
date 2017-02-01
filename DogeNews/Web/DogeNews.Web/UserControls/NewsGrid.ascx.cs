using System;
using System.Web.UI.WebControls;

using DogeNews.Web.Mvp.UserControls.NewsGrid;
using DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments;
using DogeNews.Common.Enums;
using DogeNews.Web.Models;
using DogeNews.Data.Models;
using DogeNews.Web.DataSources.Contracts;

using WebFormsMvp.Web;
using WebFormsMvp;

namespace DogeNews.Web.UserControls
{
    [PresenterBinding(typeof(NewsGridPresenter))]
    public partial class NewsGrid : MvpUserControl<NewsGridViewModel>, INewsGridView
    {
        public event EventHandler<PageLoadEventArgs> PageLoad;
        public event EventHandler<ChangePageEventArgs> ChangePage;
        public event EventHandler<OrderByEventArgs> OrderByDate;

        public IDataSource<NewsItem, NewsWebModel> NewsDataSource
        {
            get { return this.Model.NewsDataSource; }
            set { this.Model.NewsDataSource = value; }
        }

        public void ChangePageClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            int page = int.Parse(button.Text);
            var eventArgs = new ChangePageEventArgs { Page = page, ViewState = this.ViewState };

            this.ChangePage(this, eventArgs);
        }

        public void OrderByDateClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            var orderBy = (OrderByType)Enum.Parse(typeof(OrderByType), button.CommandArgument);
            var eventArgs = new OrderByEventArgs { OrderBy = orderBy, ViewState = this.ViewState };

            this.OrderByDate(this, eventArgs);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                this.PageLoad(this, new PageLoadEventArgs { IsPostBack = true, ViewState = this.ViewState });
                return;
            }

            this.PageLoad(this, new PageLoadEventArgs { IsPostBack = false, ViewState = this.ViewState });
        }
    }
}