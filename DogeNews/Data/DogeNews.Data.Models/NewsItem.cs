using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DogeNews.Web.Common.Enums;

namespace DogeNews.Data.Models
{
    public class NewsItem
    {
        private ICollection<Comment> comments;

        public NewsItem()
        {
            this.comments = new HashSet<Comment>();
            this.CreatedOn = DateTime.UtcNow;
        }

        public int Id { get; set; }

        [MinLength(5)]
        [MaxLength(200)]
        [Index(IsUnique = true)]
        public string Title { get; set; }

        [MinLength(5)]
        [MaxLength(30)]
        public string Subtitle { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public NewsCategoryType Category { get; set; }
        
        public string AuthorId { get; set; }

        public int ImageId { get; set; }
        
        public DateTime? CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsApproved { get; set; }

        public bool IsAddedByAdmin { get; set; }
        
        public virtual User Author { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}