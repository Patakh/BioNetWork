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
            public string Email { get; set; }
            [Required]
            [MinLength(5)]
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }

}
