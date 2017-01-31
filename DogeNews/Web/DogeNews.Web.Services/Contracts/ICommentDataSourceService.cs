using System.Collections.Generic;
using DogeNews.Data.Models;
using DogeNews.Web.Models;

namespace DogeNews.Web.Services.Contracts
{
    public interface ICommentDataSourceService : IDataSourceService<Comment, CommentWebModel>
    {
        IEnumerable<CommentWebModel> GetCommentsForArticleByTitle(string title);

        void AddComment(string newsItemTitle, string commentContent, string userName);
    }
}