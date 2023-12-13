using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BioNetWork.Controllers.Administration;

[Authorize("Admin")]
public class AdministrationController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
