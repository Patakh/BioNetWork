using BioNetWork.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Diagnostics;
using BioNetWork.Models.Users;
using static BioNetWork.Models.Users.UsersModel;

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

        
        public IActionResult Users()
        {
            try
            {
                UsersModel user = new UsersModel
                {
                    UserList = new List<Person>()
                };

                String connectionString = "Data Source=DESKTOP-2EN7EE8\\SQLEXPRESS;Initial Catalog=Users;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM users";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                           
                            while (reader.Read())
                            {
                                UsersModel person = new UsersModel
                                {
                                    person = new Person()
                                };
                                person.person.Id = "" + reader.GetInt32(0);
                                person.person.Name = reader.GetString(1);
                                person.person.Email = reader.GetString(2);
                                person.person.Phone = reader.GetString(3);
                                person.person.Adres = reader.GetString(4);
                                person.person.DateRegistration = reader.GetDateTime(5).ToString();
                                user.UserList.Add(person.person);
                            }
                        }
                    }
                    connection.Close();
                }
                return View("Users", user);
            }
            catch(Exception ex)
            {
                return View(new ErrorViewModel { });
            }
        }
        public IActionResult Create()
        {
            return View();

        }
        public IActionResult CreatePerson(Person person)
        {

            person.Name = Request.Form["Name"];
            person.Email = Request.Form["Email"];
            person.Phone = Request.Form["Phone"];
            person.Adres = Request.Form["Adres"];

            try
            {
                String connectionString = "Data Source=DESKTOP-2EN7EE8\\SQLEXPRESS;Initial Catalog=Users;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO users" +
                                  "(name, email, phone, address) VALUES" +
                                  "(@name, @email, @phone, @address);";
                    using(SqlCommand command= new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", person.Name);
                        command.Parameters.AddWithValue("@email", person.Email);
                        command.Parameters.AddWithValue("@phone", person.Phone);
                        command.Parameters.AddWithValue("@address", person.Adres);
                        command.ExecuteNonQuery();

                    }
                    connection.Close();
                }
               
            }
            catch(Exception ex)
            {
               
            }
            return View("Users");
        }
    }
}