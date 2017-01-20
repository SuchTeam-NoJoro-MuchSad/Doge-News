using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DogeNews.Data.Models.Contracts;
using DogeNews.Data.Models.Enumerations;

namespace DogeNews.Data.Models
{
    public class NewsItem : INewsItem
    {
        private ICollection<Comment> comments;
        private DateTime? createdOn = null;

        public NewsItem()
        {
            this.comments = new HashSet<Comment>();
            this.createdOn = DateTime.UtcNow;
        }

        public int Id { get; set; }

        [MinLength(5)]
        [MaxLength(30)]
        [Index(IsUnique = true)]
        public string Title { get; set; }

        [MinLength(5)]
        [MaxLength(30)]
        public string Subtitle { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public NewsCategoryType Category { get; set; }

        [Required]
        public User Author { get; set; }

        public Image Image { get; set; }

        public DateTime? CreatedOn
        {
            get { return this.createdOn; }

            set { this.createdOn = value; }
        }

        public DateTime? DeletedOn { get; set; }

        public bool IsApproved { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}