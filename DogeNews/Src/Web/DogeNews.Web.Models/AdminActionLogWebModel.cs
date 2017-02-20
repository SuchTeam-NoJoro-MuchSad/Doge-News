using System;

namespace DogeNews.Web.Models
{
    public class AdminActionLogWebModel
    {
        public int Id { get; set; }

        public UserWebModel User { get; set; }

        public string InvokedMethodName { get; set; }

        public string InvokedMethodArguments { get; set; }

        public DateTime Date { get; set; }
    }
}