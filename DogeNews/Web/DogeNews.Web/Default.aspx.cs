using System;

using WebFormsMvp.Web;
using WebFormsMvp;

using DogeNews.Web.MVP.Default;
using DogeNews.Web.Models;
using DogeNews.Data.Models;
using DogeNews.Web.Services.Contracts;

using Ninject;

namespace DogeNews.Web
{
    [PresenterBinding(typeof(DefaultPresenter))]
    public partial class _Default : MvpPage<DefaultViewModel>, IDefaultView
    {
        [Inject]
        public IDataSourceService<NewsItem, NewsWebModel> NewsDataSource { get; set; }

        public void Page_Load(object sender, EventArgs e)
        {
            this.NewsGrid.NewsDataSource = this.NewsDataSource;
        }
    }
}