using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class UserSaveViewModel
    {
        [NotMapped]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]

        public string LastName { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]

        public string Username { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [NotMapped]
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
        [NotMapped]
        public bool State { get; set; }
    }
}
