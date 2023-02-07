using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BioNetWork.Areas.User.Models
{
    public class LoginModel
    {
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [MinLength(5)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]

        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; } = "";

    }

}
