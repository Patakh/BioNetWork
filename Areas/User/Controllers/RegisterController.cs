using BioNetWork.Areas.User.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static BioNetWork.Areas.User.Models.RegisterModel;

namespace BioNetWork.Areas.User.Controllers
{
    public class RegisterController : RegisterModel
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> sigInManager;
        public RegisterController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> sigInManager)
        {
            this.userManager = userManager;
            this.sigInManager = sigInManager;
            this.userManager.PasswordValidators.Clear();
        }
        [Area("User")]
        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Areas/User/Pages/Register.cshtml");
        }

        [Area("User")]
        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = register.Email,
                    Email = register.Email
                };
                var result = await userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    await sigInManager.SignInAsync(user, false);

                    return View("~/Areas/Blogs/Views/Home/Index.cshtml");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("~/Areas/User/Pages/Register.cshtml", register);
        }

    }
}
