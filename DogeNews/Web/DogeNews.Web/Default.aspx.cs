using System;

using WebFormsMvp.Web;
using WebFormsMvp;
using DogeNews.Web.Mvp.Default;

namespace DogeNews.Web
{
    [PresenterBinding(typeof(DefaultPresenter))]
    public partial class _Default : MvpPage<DefaultViewModel>, IDefaultView
    {
        public event EventHandler PageLoad;

        public void Page_Load(object sender, EventArgs e)
        {
            this.PageLoad(this, e);
            this.NewsSlider.News = this.Model.SliderNews;
        }
    }
}