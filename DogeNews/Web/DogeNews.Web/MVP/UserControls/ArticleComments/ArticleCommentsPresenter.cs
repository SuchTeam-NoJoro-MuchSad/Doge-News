using DogeNews.Web.MVP.UserControls.ArticleComments.EventArguments;
using DogeNews.Web.Services.Contracts;

using WebFormsMvp;

namespace DogeNews.Web.MVP.UserControls.ArticleComments
{
    public class ArticleCommentsPresenter : Presenter<IArticleCommentsView>
    {
        private const int PageSize = 6;

        private IArticleCommentsService articleCommentsService;

        public ArticleCommentsPresenter(IArticleCommentsView view,
            IArticleCommentsService dataSourceService)
            : base(view)
        {
            this.articleCommentsService = dataSourceService;
            this.View.PageLoad += this.PageLoad;
            this.View.AddComment += this.AddComment;
        }

        public void PageLoad(object sender, ArticleCommetnsPageLoadEventArgs eventArgs)
        {
            this.View.Model.Comments = this.articleCommentsService.GetCommentsForArticleByTitle(eventArgs.Title);
        }

        public void AddComment(object sender, AddCommentEventArguments addCommentEventArguments)
        {
            this.articleCommentsService.AddComment(
                addCommentEventArguments.ArticleTitle,
                addCommentEventArguments.Content,
                addCommentEventArguments.Username);
            this.View.Model.Comments =
                this.articleCommentsService.GetCommentsForArticleByTitle(addCommentEventArguments.ArticleTitle);
        }
    }
}