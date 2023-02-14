using BioNetWork.Areas.User.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Win32;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BioNetWork.Areas.User.Controllers
{
    public class RegisterController : Controller
    {
        const string ConnectionStrings ="Data Source=DESKTOP-2EN7EE8\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        [Area("User")]
        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Areas/User/Views/Accounting/Register.cshtml");
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

                 string sqlExpression = "InsertUser";
                 using (SqlConnection connection = new SqlConnection(ConnectionStrings))
                 {
                     connection.Open();
                     SqlCommand command = new SqlCommand(sqlExpression, connection);
                     command.CommandType = CommandType.StoredProcedure;
                     SqlParameter passwordParam = new SqlParameter
                     {
                         ParameterName = "@Password",
                         Value = newUser.Password
                     };
                     command.Parameters.Add(passwordParam);
                     SqlParameter emailParam = new SqlParameter
                     {
                         ParameterName = "@Email",
                         Value = newUser.Email
                     };
                     command.Parameters.Add(emailParam);
                     newUser.Id = (Guid)command.ExecuteScalar();
                     connection.Close();
                     return UserAccout(newUser);
                 }
             }
             else
             {
                 ModelState.AddModelError("", "Гуляй поле");
                 return View("~/Areas/User/Views/Accounting/Register.cshtml", registerData);
             }

        }

        [Area("User")]
        [HttpPost]
        public IActionResult UserAccout(UserModel loginData)
        {
            return View("~/Areas/User/Views/Account/UserAccount.cshtml", loginData);
        }
    }
}
