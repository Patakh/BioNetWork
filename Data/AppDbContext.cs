using BioNetWork.Model.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BioNetWork.Data;
public class AppDbContext: IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
    {
        Database.EnsureCreated();
    }
}

