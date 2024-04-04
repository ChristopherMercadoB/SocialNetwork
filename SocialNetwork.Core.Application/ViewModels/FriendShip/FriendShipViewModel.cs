

namespace SocialNetwork.Core.Application.ViewModels.FriendShip
{
    public class FriendShipViewModel
    {
        public int Id { get; set; }
        public int UserFriendId { get; set; }
        public int CurrentUserId { get; set; }
        public string FriendUsername { get; set; }
        public string FriendImageUrl { get; set; }
    }
}
