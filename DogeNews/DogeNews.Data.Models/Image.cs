using System.ComponentModel.DataAnnotations;

namespace DogeNews.Data.Models
{
    public class Image 
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string FullName { get; set; }

        [MaxLength(200)]
        public string Url { get; set; }

        [MaxLength(10)]
        public string FileExtention { get; set; }
    }
}