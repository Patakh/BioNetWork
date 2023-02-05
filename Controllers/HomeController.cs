using BioNetWork.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Diagnostics;
using static BioNetWork.Models.Users.UsersModel;

namespace BioNetWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public String connectionString = "Data Source=DESKTOP-2EN7EE8\\SQLEXPRESS;Initial Catalog=Users;Integrated Security=True";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult MyPublications()
        {
            return View();
        }
        public IActionResult Friends()
        {
            return View();

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
