using System;

using DogeNews.Web.Services.Contracts;
using DogeNews.Common.Validators;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.Default
{
    public class DefaultPresenter : Presenter<IDefaultView>
    {
        private readonly INewsService newsService;

        public DefaultPresenter(IDefaultView view, INewsService newsService) 
            : base(view)
        {
            Validator.ValidateThatObjectIsNotNull(newsService, nameof(newsService));

            this.newsService = newsService;

            this.View.PageLoad += this.PageLoad;
        }

        public void PageLoad(object sender, EventArgs e)
        {
            Validator.ValidateThatObjectIsNotNull(e, nameof(e));

            this.View.Model.SliderNews = this.newsService.GetSliderNews();
        }
    }
}