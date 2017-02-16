using System;
using System.Web.UI.WebControls;

using DogeNews.Web.Mvp.UserControls.NewsGrid;
using DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments;
using DogeNews.Common.Enums;

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
        public event EventHandler<OnArticleDeleteEventArgs> ArticleDelete;
        public event EventHandler<OnArticleEditEventArgs> ArticleEdit;
        public event EventHandler<OnArticleRestoreEventArgs> ArticleRestore;

        public void ChangePageClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int page = int.Parse(button.Text);
            ChangePageEventArgs eventArgs = new ChangePageEventArgs
            {
                Page = page,
                ViewState = this.ViewState,
                IsAdminUser = Context.User.IsInRole(Common.Constants.Roles.Admin)
            };

            this.ChangePage(this, eventArgs);
        }

        public void OrderByDateClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            OrderByType orderBy = (OrderByType)Enum.Parse(typeof(OrderByType), button.CommandArgument);
            OrderByEventArgs eventArgs = new OrderByEventArgs
            {
                OrderBy = orderBy,
                ViewState = this.ViewState,
                IsAdminUser = Context.User.IsInRole(Common.Constants.Roles.Admin)
            };

            this.OrderByDate(this, eventArgs);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                this.PageLoad(this, new PageLoadEventArgs
                {
                    IsPostBack = true,
                    IsAdminUser = Context.User.IsInRole(Common.Constants.Roles.Admin),
                    ViewState = this.ViewState,
                    QueryString = this.Page.ClientQueryString
                });
            }
            else
            {
                this.PageLoad(this, new PageLoadEventArgs
                {
                    IsPostBack = false,
                    IsAdminUser = Context.User.IsInRole(Common.Constants.Roles.Admin),
                    ViewState = this.ViewState,
                    QueryString = this.Page.ClientQueryString
                });
            }
        }

        protected void ArticleDeleteButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string newsItemId = button.CommandArgument;

            OnArticleDeleteEventArgs eventArgs = new OnArticleDeleteEventArgs
            {
                NewsItemId = newsItemId
            };

            this.ArticleDelete(this, eventArgs);
        }

        protected void ArticleEditButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string newsItemId = button.CommandArgument;

            OnArticleEditEventArgs eventArgs = new OnArticleEditEventArgs
            {
                IsAdminUser = this.Context.User.IsInRole(Common.Constants.Roles.Admin),
                NewsItemId = newsItemId
            };

            this.ArticleEdit(this, eventArgs);
        }

        protected void ArticleRestoreButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string newsItemId = button.CommandArgument;

            OnArticleRestoreEventArgs eventArgs = new OnArticleRestoreEventArgs
            {
                NewsItemId = newsItemId
            };

            this.ArticleRestore(this, eventArgs);
        }
    }
}