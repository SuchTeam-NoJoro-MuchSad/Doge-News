using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogeNews.Data.Models
{
    public class User
    {
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
        [MaxLength(20)]
        public string Email { get; set; }

        [Required]
        public string Salt { get; set; }

        [Required]
        public string PassHash { get; set; }
    }
}