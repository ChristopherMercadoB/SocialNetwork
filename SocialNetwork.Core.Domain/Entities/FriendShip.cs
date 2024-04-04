using SocialNetwork.Core.Domain.Common;

namespace SocialNetwork.Core.Domain.Entities
{
    public class FriendShip:AuditableBaseEntity
    {
        public int UserFriendId { get; set; }
        public int CurrentUserId { get; set; }
        public User UserFriend { get; set; }

    }
}
