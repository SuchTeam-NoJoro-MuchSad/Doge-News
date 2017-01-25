using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DogeNews.Web.Common.Enums;

namespace DogeNews.Data.Models
{
    public class User 
    {
        private ICollection<NewsItem> newsItems;
        private ICollection<Comment> comments;

        public User()
        {
            this.UserRole = UserRoleType.Normal;
            this.newsItems = new HashSet<NewsItem>();
            this.comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Salt { get; set; }

        [Required]
        public string PassHash { get; set; }

        [Required]
        public UserRoleType UserRole { get; set; }

        public virtual ICollection<NewsItem> NewsItems
        {
            get { return this.newsItems; }
            set { this.newsItems = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}