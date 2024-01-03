using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BioNetWork.Model.Account;
public class RegisterModel
{
    [Required]
    [EmailAddress]
    [Display(Name ="Email")]
    public string Email { get; set; }

    [Required]
    [Display(Name ="Пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [Display(Name = "Потвердите пароль")]
    [Compare("Password",ErrorMessage ="Пароли не совпадают")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set;}
}

