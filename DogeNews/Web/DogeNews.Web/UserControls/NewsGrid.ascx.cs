using System;
using System.Web.UI.WebControls;

using DogeNews.Web.MVP.UserControls.NewsGrid;
using DogeNews.Web.MVP.UserControls.NewsGrid.EventArguments;

using WebFormsMvp.Web;
using WebFormsMvp;

namespace DogeNews.Web.UserControls
{
    [PresenterBinding(typeof(NewsGridPresenter))]
    public partial class NewsGrid : MvpUserControl<NewsGridViewModel>, INewsGridView
    {
        public event EventHandler PageLoad;
        public event EventHandler<ChangePageEventArgs> ChangePage;

        public void ChangePageClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            int page = int.Parse(button.Text);
            var eventArgs = new ChangePageEventArgs { Page = page };

            this.ChangePage(this, eventArgs);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.PageLoad(this, null);
        }
    }
}