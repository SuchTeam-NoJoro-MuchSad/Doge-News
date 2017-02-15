using DogeNews.Common.Validators;
using DogeNews.Web.Mvp.UserControls.ArticleComments.EventArguments;
using DogeNews.Web.Services.Contracts;

using WebFormsMvp;

namespace DogeNews.Web.Mvp.UserControls.ArticleComments
{
    public class ArticleCommentsPresenter : Presenter<IArticleCommentsView>
    {
        private const int PageSize = 6;

        private IArticleCommentsService articleCommentsService;

        public ArticleCommentsPresenter(
            IArticleCommentsView view,
            IArticleCommentsService articleCommentsService)
            : base(view)
        {
            Validator.ValidateThatObjectIsNotNull(articleCommentsService, nameof(articleCommentsService));

            this.articleCommentsService = articleCommentsService;
            this.View.PageLoad += this.PageLoad;
            this.View.AddComment += this.AddComment;
        }

        public void PageLoad(object sender, ArticleCommetnsPageLoadEventArgs e)
        {
            Validator.ValidateThatObjectIsNotNull(e, nameof(e));

            this.View.Model.Comments = this.articleCommentsService.GetCommentsForArticleByTitle(e.Title);
        }

        public void AddComment(object sender, AddCommentEventArguments e)
        {
            Validator.ValidateThatObjectIsNotNull(e, nameof(e));

            this.articleCommentsService.AddComment(
                e.ArticleTitle,
                e.Content,
                e.Username);
            this.View.Model.Comments = this.articleCommentsService.GetCommentsForArticleByTitle(e.ArticleTitle);
        }
    }
}