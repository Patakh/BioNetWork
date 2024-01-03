using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BioNetWork.Model.Account;

public class User : IdentityUser
{
    [DataType(DataType.ImageUrl)]
    public string? ImageUrl { get; set; }
}

