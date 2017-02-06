using System;

using DogeNews.Web.Services.Contracts;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.Default
{
    public class DefaultPresenter : Presenter<IDefaultView>
    {
        private readonly INewsService newsService;

        public DefaultPresenter(IDefaultView view, INewsService newsService) 
            : base(view)
        {
            this.newsService = newsService;

            this.View.PageLoad += this.PageLoad;
        }

        public void PageLoad(object sender, EventArgs e)
        {
            var sliderNews = this.newsService.GetSliderNews();
            this.View.Model.SliderNews = sliderNews;
        }
    }
}