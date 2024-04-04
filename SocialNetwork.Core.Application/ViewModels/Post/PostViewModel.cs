
namespace SocialNetwork.Core.Application.ViewModels.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string? ImageUser { get; set; }
        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
