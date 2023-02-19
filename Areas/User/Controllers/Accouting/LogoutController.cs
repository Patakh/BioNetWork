using Microsoft.AspNetCore.Mvc;

namespace BioNetWork.Areas.User.Controllers.Accouting
{
    public class LogoutController : Controller
    {
        public IActionResult Login()
        {
             
            return View("~/Login.cshtml");
        }
    }
}