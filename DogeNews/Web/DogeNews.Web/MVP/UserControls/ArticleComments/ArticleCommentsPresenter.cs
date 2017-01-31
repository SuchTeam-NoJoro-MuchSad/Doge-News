using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.MVP.News.Article.EventArguments;
using DogeNews.Web.MVP.UserControls.ArticleComments.EventArguments;
using DogeNews.Web.Services.Contracts;
using WebFormsMvp;

namespace DogeNews.Web.MVP.UserControls.ArticleComments
{
    public class ArticleCommentsPresenter : Presenter<IArticleCommentsView>
    {
        private const int PageSize = 6;

        private ICommentDataSourceService dataSourceService;

        public ArticleCommentsPresenter(IArticleCommentsView view,
            ICommentDataSourceService dataSourceService)
            : base(view)
        {
            this.dataSourceService = dataSourceService;
            this.View.PageLoad += this.PageLoad;
            this.View.AddComment += this.AddComment;
        }

        public void PageLoad(object sender, ArticleCommetnsPageLoadEventArgs eventArgs)
        {
            this.View.Model.Comments = this.dataSourceService.GetCommentsForArticleByTitle(eventArgs.Title);
        }

        public void AddComment(object sender, AddCommentEventArguments addCommentEventArguments)
        {
            this.dataSourceService.AddComment(addCommentEventArguments.ArticleTitle,
                addCommentEventArguments.Content, addCommentEventArguments.Username);
        }
    }
}