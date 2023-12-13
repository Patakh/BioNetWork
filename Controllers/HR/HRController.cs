using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BioNetWork.Controllers.HR;
[Authorize("PolicyHR")]
public class HRController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

