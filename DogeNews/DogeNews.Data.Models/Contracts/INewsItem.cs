using System;
using System.Collections.Generic;

using DogeNews.Data.Models.Enumerations;

namespace DogeNews.Data.Models.Contracts
{
    public interface INewsItem
    {
        int Id { get; set; }
        string Title { get; set; }
        string Subtitle { get; set; }
        string Content { get; set; }
        NewsCategoryType Category { get; set; }
        User Author { get; set; }
        Image Image { get; set; }
        DateTime? CreatedOn { get; set; }
        DateTime? DeletedOn { get; set; }
        bool IsApproved { get; set; }
        ICollection<Comment> Comments { get; set; }
    }
}