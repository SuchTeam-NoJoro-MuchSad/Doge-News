using System;
using System.ComponentModel.DataAnnotations;

namespace DogeNews.Data.Models
{
    public class AdminActionLog
    {
        public AdminActionLog()
        {
            this.Date = DateTime.UtcNow;
        }

        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public string InvokedMethodName { get; set; }

        [Required]
        public string InvokedMethodArguments { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}