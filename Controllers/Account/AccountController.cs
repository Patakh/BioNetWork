using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using BioNetWork.Models;

namespace BioNetWork.Controllers.Account;

public class AccountController : Controller
{
    public IActionResult AccessDenied()
    {
        return View();
    }
    public IActionResult Login()
    {
        ClaimsPrincipal claimsPrincipal = HttpContext.User;
        if (claimsPrincipal.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(VMLogin vMLogin)
    {
        if (vMLogin.Email == "user@xample.com" && vMLogin.Password == "123")
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, vMLogin.Email),
                new Claim("OtherProperties", "Example Role"),
                new Claim("Department","HR"),
                new Claim("Admin","true"),
                new Claim("Manager","true"),
                new Claim("EmploymentDate","12.08.2023")
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = vMLogin.RememberMe,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);

            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }
}
