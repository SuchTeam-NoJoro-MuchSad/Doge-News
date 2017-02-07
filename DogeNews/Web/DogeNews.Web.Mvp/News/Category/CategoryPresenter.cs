using DogeNews.Web.Mvp.News.Category.EventArguments;
using DogeNews.Web.Services.Contracts;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.News.Category
{
    public class CategoryPresenter : Presenter<ICategoryView>
    {
        private INewsService newsService;

        public CategoryPresenter(ICategoryView view, INewsService newsService) 
            : base(view)
        {
            this.newsService = newsService;

            this.View.PageLoad += this.PageLoad;
        }

        public void PageLoad(object sender, CategoryPageLoadEventArgs e)
        {
            string category = this.HttpContext.Request.QueryString["name"];
            
            this.View.Model.News = this.newsService.GetNewsItemsByCategory(category);
        }
    }
}