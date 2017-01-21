using System.ComponentModel.DataAnnotations;

using DogeNews.Data.Models.Contracts;

namespace DogeNews.Data.Models
{
    public class Comment : IComment
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