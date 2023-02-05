using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BioNetWork.Areas.User.Models
{
    public class LoginModel : Controller
    {
        public class Login
        {
            [Required]
            [DataType(DataType.EmailAddress)]
            [MaxLength(25)]
            [MinLength(5)]
            public string Email { get; set; }
            [Required]
            [MinLength(5)]
            [MaxLength(25)]
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }
    }
}
