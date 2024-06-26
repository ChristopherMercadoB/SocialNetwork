﻿

using SocialNetwork.Core.Domain.Common;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Comment:AuditableBaseEntity
    {
        public string Description { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
        public List<Reply> Replies { get; set; }
    }
}
