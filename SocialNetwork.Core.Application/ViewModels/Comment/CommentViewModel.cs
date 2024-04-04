using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string? UserImage { get; set; }
        public string Username { get; set; }
    }
}
