using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BioNetWork.Areas.User.Models
{
    public class RegisterModel
        {
            public Guid Id { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            [MaxLength(25)]
            [MinLength(5)]
            public string Email { get; set; }
            [Required]
            [MinLength(5)]
            [MaxLength(25)]
            public string Password { get; set; }
            [Required]
            [MinLength(5)]
            [MaxLength(25)]
            [Compare(nameof(Password), ErrorMessage = "Пароли не совподают")]
            public string CnnfirmPassword { get; set; }

    }
}
