using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DogeNews.Data.Models
{
    public class Image 
    {
        private ICollection<NewsItem> newsItems;

        public Image()
        {
            this.newsItems = new HashSet<NewsItem>();
        }

        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string FullName { get; set; }
        
        [MaxLength(10)]
        public string FileExtention { get; set; }

        public virtual ICollection<NewsItem> NewsItems
        {
            get { return this.newsItems; }
            set { this.newsItems = value; }
        }
    }
}