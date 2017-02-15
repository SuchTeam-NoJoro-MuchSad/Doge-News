using System;
using System.ComponentModel.DataAnnotations;

namespace DogeNews.Data.Models
{
    public class Comment 
    {
        public Comment()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(250)]
        public string Content { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}