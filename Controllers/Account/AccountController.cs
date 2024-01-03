using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using BioNetWork.Model.Account;
using Microsoft.AspNetCore.Identity;

namespace BioNetWork.Controllers.Account;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel registerModel)
    {
        if (ModelState.IsValid)
        {
            User user = new() {Email = registerModel.Email, UserName = registerModel.Email };
            var result = await _userManager.CreateAsync(user,registerModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user,false);
                return RedirectToAction("Index","Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        } 
        return View(registerModel);
    }

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
    public async Task<IActionResult> Login(LoginModel vMLogin)
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
