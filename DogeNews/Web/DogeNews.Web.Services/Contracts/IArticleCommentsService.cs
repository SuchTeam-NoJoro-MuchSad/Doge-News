using System.Collections.Generic;
using DogeNews.Web.Models;

namespace DogeNews.Web.Services.Contracts
{
    public interface IArticleCommentsService
    {
        IEnumerable<CommentWebModel> GetCommentsForArticleByTitle(string title);

        void AddComment(string newsItemTitle, string commentContent, string userName);
    }
}