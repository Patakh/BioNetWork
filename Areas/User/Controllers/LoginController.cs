using BioNetWork.Areas.User.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NuGet.Protocol.Plugins;
using System.Linq;

namespace BioNetWork.Areas.User.Controllers
{
    public class LoginController : Controller
    {
       
        [Area("User")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Areas/User/Views/Login.cshtml");
        }

        [Area("User")]
        [HttpPost]
        public IActionResult Login(LoginModel loginData)
        {

            if (ModelState.IsValid)
            {
               
                  /* if (idPerson != 0)
                   {
                       return UserAccout();
                   }
                   else
                   {
                       ModelState.AddModelError("", "Гуляй поле");
                       return View("~/Areas/User/Views/Login.cshtml", loginData);
                   };*/
            }
            else
                ModelState.AddModelError("", "Гуляй поле");
            return View("~/Areas/User/Views/Login.cshtml", loginData);
        }

        [Area("User")]
        [HttpPost]
        public IActionResult UserAccout(UserModel loginData)
        {
            return View("~/Areas/User/Views/UserAccount.cshtml", loginData);
        }
    }
}
