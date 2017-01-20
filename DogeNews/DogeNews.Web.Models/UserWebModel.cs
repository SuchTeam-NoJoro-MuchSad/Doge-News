namespace DogeNews.Web.Models
{
    public class UserWebModel
    {
        public int Id { get; set; }
        
        public string Username { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public byte[] Salt { get; set; }
        
        public string Password { get; set; }
    }
}
