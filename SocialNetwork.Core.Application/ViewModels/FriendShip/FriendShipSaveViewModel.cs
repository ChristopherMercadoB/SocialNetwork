using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.FriendShip
{
    public class FriendShipSaveViewModel
    {
        public int Id { get; set; }
        public int UserFriendId { get; set; }
        public int CurrentUserId { get; set; }
    }
}
