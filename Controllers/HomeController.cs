using BioNetWork.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Diagnostics;
using BioNetWork.Models.Users;
using static BioNetWork.Models.Users.UsersModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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


        public IActionResult Users()
        {
            try
            {
                UsersModel user = new UsersModel
                {
                    UserList = new List<Person>()
                };
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
                                person.person.DateRegistration = reader.GetDateTime(5);
                                user.UserList.Add(person.person);
                            }
                        }
                    }
                    connection.Close();
                }
                return View("Users", user);
            }
            catch (Exception ex)
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO users" +
                                  "(name, email, phone, address) VALUES" +
                                  "(@name, @email, @phone, @address);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", person.Name);
                        command.Parameters.AddWithValue("@email", person.Email);
                        command.Parameters.AddWithValue("@phone", person.Phone);
                        command.Parameters.AddWithValue("@address", person.Adres);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return Users();
            }
            catch (Exception ex)
            {
                return Users();
            }
        }



        public IActionResult SelectEditPersonData(string Id)
        {
           // String personId = Request.Query["Id"];

            try
            {
                Person person = new Person();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM users WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                person.Id = "" + reader.GetInt32(0);
                                person.Name = reader.GetString(1);
                                person.Email = reader.GetString(2);
                                person.Phone = reader.GetString(3);
                                person.Adres = reader.GetString(4);
                                person.DateRegistration = reader.GetDateTime(5);
                            }
                        }
                    }
                    connection.Close();
                }
                return View("Edit", person);
            }
            catch (Exception ex)
            {
                return View(new ErrorViewModel { });
            }
        }

        public IActionResult EditPersonData(Person person)
        {
            person.Id = Request.Form["Id"];
            person.Name = Request.Form["Name"];
            person.Email = Request.Form["Email"];
            person.Phone = Request.Form["Phone"];
            person.Adres = Request.Form["Adres"];

            try
            {
               using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE users " +
                        "SET name=@name, email=@email, phone=@phone, address=@address " +
                        "WHERE id=@id";
                    using(SqlCommand command= new SqlCommand(sql,connection)) {
                        command.Parameters.AddWithValue("@id", person.Id);
                        command.Parameters.AddWithValue("@name", person.Name);
                        command.Parameters.AddWithValue("@email", person.Email);
                        command.Parameters.AddWithValue("@phone", person.Phone);
                        command.Parameters.AddWithValue("@address", person.Adres);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return Users();
            }
            catch (Exception ex)
            {
              return Users();
            }
        }
        public IActionResult Delete(string Id)
        {
            // String personId = Request.Query["Id"];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "DELETE FROM users WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", Id);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return Users();
            }
            catch (Exception ex)
            {
              return Error();
            }
        }
    }
}
