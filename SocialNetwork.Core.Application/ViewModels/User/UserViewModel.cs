namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string? ImageUrl { get; set; }
        public bool State { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
