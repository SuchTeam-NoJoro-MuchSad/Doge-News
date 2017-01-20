using System.Collections.Generic;
using DogeNews.Data.Models.Enumerations;

namespace DogeNews.Data.Models.Contracts
{
    public interface IUser
    {
        int Id { get; set; }
        string Username { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Salt { get; set; }
        string PassHash { get; set; }
        UserRoleType UserRole { get; set; }
        ICollection<NewsItem> NewsItems { get; set; }
        ICollection<Comment> Comments { get; set; }
    }
}