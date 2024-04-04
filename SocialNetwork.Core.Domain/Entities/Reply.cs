

using SocialNetwork.Core.Domain.Common;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Reply:AuditableBaseEntity
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public Comment Comment { get; set; }
    }
}
