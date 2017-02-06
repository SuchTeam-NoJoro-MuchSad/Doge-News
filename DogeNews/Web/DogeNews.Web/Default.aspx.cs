using System;

using WebFormsMvp.Web;
using WebFormsMvp;

using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.Mvp.Default;
using DogeNews.Web.DataSources.Contracts;

using Ninject;

namespace DogeNews.Web
{
    [PresenterBinding(typeof(DefaultPresenter))]
    public partial class _Default : MvpPage<DefaultViewModel>, IDefaultView
    {
        [Inject]
        [Named("_Default")]
        public IDataSource<NewsItem, NewsWebModel> NewsDataSource { get; set; }

        public event EventHandler PageLoad;

        public void Page_Load(object sender, EventArgs e)
        {
            this.NewsGrid.NewsDataSource = this.NewsDataSource;
            this.PageLoad(this, e);
            this.NewsSlider.News = this.Model.SliderNews;
        }
    }
}