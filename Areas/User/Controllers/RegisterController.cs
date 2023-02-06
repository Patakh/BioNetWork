using BioNetWork.Areas.User.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace BioNetWork.Areas.User.Controllers
{
    public class RegisterController : Controller
    {
        [Area("User")]
        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Areas/User/Views/Register.cshtml");
        }

        [Area("User")]
        [HttpPost]
        public IActionResult Register(RegisterModel registerData)
        {
            if(ModelState.IsValid)
            {
                UserModel newUser = new UserModel();
                newUser.Email = registerData.Email;
                newUser.Password = registerData.Password;
                newUser.Id = Guid.NewGuid();
                newUser.DataRegister = DateTime.Now;
                return UserAccout(newUser);
            }

            ModelState.AddModelError("", "Гуляй поле");

            return View("~/Areas/User/Views/Register.cshtml", registerData);
        }

        [Area("User")]
        [HttpPost]
        public IActionResult UserAccout(UserModel loginData)
        {
            return View("~/Areas/User/Views/UserAccount.cshtml", loginData);
        }
    }
}
