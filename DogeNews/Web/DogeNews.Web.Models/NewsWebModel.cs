using System;

using DogeNews.Web.Common.Enums;

namespace DogeNews.Web.Models
{
    public class NewsWebModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public NewsCategoryType Category { get; set; }

        public string Content { get; set; }

        public bool IsAddedByAdmin { get; set; }

        public UserWebModel Author { get; set; }    
        
        public ImageWebModel Image { get; set; }    

        public DateTime? CreatedOn { get; set; }
    }
}
