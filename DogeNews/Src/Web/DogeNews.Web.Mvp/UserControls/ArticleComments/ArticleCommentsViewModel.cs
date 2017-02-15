using System.Collections.Generic;

using DogeNews.Web.Models;

namespace DogeNews.Web.Mvp.UserControls.ArticleComments
{
    public class ArticleCommentsViewModel
    {
        public IEnumerable<CommentWebModel> Comments { get; set; }
    }
}