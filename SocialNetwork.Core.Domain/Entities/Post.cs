using SocialNetwork.Core.Domain.Common;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Post:AuditableBaseEntity
    {
        public string?  ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
