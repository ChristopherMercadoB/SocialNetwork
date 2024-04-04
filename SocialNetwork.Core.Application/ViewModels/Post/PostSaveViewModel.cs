
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Core.Application.ViewModels.Post
{
    public class PostSaveViewModel
    {
        [NotMapped]
        public int Id { get; set; }
        [NotMapped]
        public string? ImageUrl { get; set; }

        [NotMapped]
        public string? VideoUrl { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Description { get; set; }
        [NotMapped]
        public int UserId { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
