using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BioNetWork.Model.Account;
public class LoginModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}
