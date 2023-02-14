using BioNetWork.Areas.User.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BioNetWork.Areas.User.Controllers
{
    public class LoginController : Controller
    {
        const string ConnectionStrings = "Data Source=DESKTOP-2EN7EE8\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        [Area("User")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Areas/User/Views/Accounting/Login.cshtml");
        }

        [Area("User")]
        [HttpPost]
        public IActionResult Login(LoginModel loginData)
        {

            if (ModelState.IsValid)
            {
                string sqlExpression = "GetUser";
                using (SqlConnection connection = new SqlConnection(ConnectionStrings))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter passwordParam = new SqlParameter
                    {
                        ParameterName = "@Password",
                        Value = loginData.Password
                    };
                    command.Parameters.Add(passwordParam);

                    SqlParameter emailParam = new SqlParameter
                    {
                        ParameterName = "@Email",
                        Value = loginData.Email
                    };
                    command.Parameters.Add(emailParam);
                    // var user = command.ExecuteScalar();
                    var userData = command.ExecuteReader();
                    if (userData.Read())
                    {
                        UserModel user = new UserModel();
                        user.Id = userData.GetGuid(0);
                        user.Name = userData.GetValue(1).ToString();
                        user.Email = userData.GetString(2);
                        user.Password = userData.GetString(3);
                        user.Biography = userData.GetValue(4).ToString();
                        user.Age = userData.GetValue(5).ToString();
                        user.Gender = userData.GetValue(6).ToString();
                        user.DataRegister = userData.GetDateTime(7);

                        try
                        {
                            user.Avatar = (byte[])userData.GetValue(8);
                        }
                        catch { };

                        user.Role = userData.GetValue(9).ToString();
                        connection.Close();
                        return UserAccout(user);

                    }
                    else
                    {
                        connection.Close();

                        ModelState.AddModelError("", "Гуляй поле");
                        return View("~/Areas/User/Views/Accounting/Login.cshtml", loginData);
                    }
                }
            }
            else
                ModelState.AddModelError("", "Гуляй поле");
            return View("~/Areas/User/Views/Accounting/Login.cshtml", loginData);
        }

        [Area("User")]
        [HttpPost]
        public IActionResult UserAccout(UserModel loginData)
        {
            return View("~/Areas/User/Views/Account/UserAccount.cshtml", loginData);
        }
    }
}
