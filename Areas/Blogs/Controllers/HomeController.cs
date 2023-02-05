using Microsoft.AspNetCore.Mvc;

namespace BioNetWork.Areas.Blogs.Controllers
{
    public class HomeController : Controller
    {
        [Area("Blogs")]
        public IActionResult Index()
        {
            return View("~/Areas/Blogs/Views/Home/Index.cshtml");
        }
    }
}
