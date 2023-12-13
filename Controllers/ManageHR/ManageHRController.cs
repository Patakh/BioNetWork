using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BioNetWork.Controllers.ManageHR;
[Authorize("ManageHR")]
public class ManageHRController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

