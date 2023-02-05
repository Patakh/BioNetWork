using BioNetWork.Areas.User.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using static BioNetWork.Areas.User.Models.RegisterModel;

namespace BioNetWork.Areas.User.Controllers
{
    public class LoginController : LoginModel
    {
        private SignInManager<IdentityUser> sigInManager;
        public LoginController(SignInManager<IdentityUser> sigInManager)
        {
            this.sigInManager = sigInManager;
        }

        [Area("User")]
        public IActionResult Login()
        {
            return View("~/Areas/User/Pages/Login.cshtml");
        }

        [Area("User")]
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {

            if (ModelState.IsValid)
            {
                var identityResult = await sigInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, true);
                var identityResult = await sigInManager.UserManager.Users.

                if (identityResult.Succeeded)
                {


                }

                ModelState.AddModelError("", "*");
            }
            return View("~/Areas/User/Pages/Login.cshtml", login);
        }
    }
}
