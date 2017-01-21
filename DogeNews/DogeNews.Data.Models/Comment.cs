using System.ComponentModel.DataAnnotations;

namespace DogeNews.Data.Models
{
    public class Comment 
    {
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(250)]
        public string Content { get; set; }
    }
}