using BioNetWork.Areas.User.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Protocol.Plugins;

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
                List<UserModel> userModelsList = new List<UserModel>();
                UserModel newUserModel = new UserModel();
                newUserModel.Email = "abdulpatakh@gmail.com1";
                newUserModel.Password = "ppxaagas1";
                newUserModel.DataRegister = DateTime.Now;
                newUserModel.Id = Guid.NewGuid();
                userModelsList.Add(newUserModel);

                newUserModel.Email = "abdulpatakh@gmail.com2";
                newUserModel.Password = "ppxaagas2";
                newUserModel.DataRegister = DateTime.Now;
                newUserModel.Id = Guid.NewGuid();
                userModelsList.Add(newUserModel);

                newUserModel.Email = "abdulpatakh@gmail.com3";
                newUserModel.Password = "ppxaagas3";
                newUserModel.DataRegister = DateTime.Now;
                newUserModel.Id = Guid.NewGuid();
                userModelsList.Add(newUserModel);

                UserModel UserModelData = new UserModel();

                foreach (UserModel userItem in userModelsList)
                {
                    if (userItem.Password == loginData.Password && userItem.Email == loginData.Email)
                    {
                       return UserAccout(userItem);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Гуляй поле");
                        return View("~/Areas/User/Views/Login.cshtml", loginData);
                    };

                }
            }

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