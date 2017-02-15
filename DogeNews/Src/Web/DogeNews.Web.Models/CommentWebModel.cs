using System;

namespace DogeNews.Web.Models
{
    public class CommentWebModel
    {
        public int Id { get; set; }

        public UserWebModel User { get; set; }

        public string Content { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}