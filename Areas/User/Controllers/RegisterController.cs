using BioNetWork.Areas.User.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Win32;
using Microsoft.Data.SqlClient;

namespace BioNetWork.Areas.User.Controllers
{
    public class RegisterController : Controller
    {
        const string ConnectionStrings ="Data Source=DESKTOP-2EN7EE8\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        [Area("User")]
        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Areas/User/Views/Register.cshtml");
        }

        [Area("User")]
        [HttpPost]
        public  IActionResult Register(RegisterModel registerData)
        {
            if (ModelState.IsValid)
            {
                UserModel newUser = new UserModel();
                newUser.Email = registerData.Email;
                newUser.Password = registerData.Password;
                newUser.DataRegister = DateTime.Now;
              
                    using (SqlConnection connection = new SqlConnection(ConnectionStrings))
                    {
                        connection.Open();
                        string sql = "INSERT INTO users_data" +
                            "(Email, Password, DataRegister) VALUES "+
                            "(@Email, @Password, @DataRegister);";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Email", newUser.Email);
                            command.Parameters.AddWithValue("@Password", newUser.Password);
                            command.Parameters.AddWithValue("@DataRegister", newUser.DataRegister);

                            command.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                    return UserAccout(newUser);
              
            }
            else
            {
                ModelState.AddModelError("", "Гуляй поле");
                return View("~/Areas/User/Views/Register.cshtml", registerData);
            }
        }

        [Area("User")]
        [HttpPost]
        public IActionResult UserAccout(UserModel loginData)
        {
            return View("~/Areas/User/Views/UserAccount.cshtml", loginData);
        }
    }
}
