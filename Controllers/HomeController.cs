using BioNetWork.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Diagnostics;
using BioNetWork.Models.Users;

namespace BioNetWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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

        public UsersModel users = new UsersModel();
        public IActionResult Users()
        {
            String connectionString = "Data Source=DESKTOP-2EN7EE8\\SQLEXPRESS;Initial Catalog=users;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "SELECT * FROM personal_data";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                           

                            users.id = "" + reader.GetInt32(0);
                            users.name = reader.GetString(1);
                            users.email = reader.GetString(2);
                            users.phone = reader.GetString(3);
                            users.address = reader.GetString(4);
                            users.date_of_registration = reader.GetDateTime(5).ToString();
                           
                        }

                    }
                }
                connection.Close();
            }
            return View("Users", users);
        }

    }
}