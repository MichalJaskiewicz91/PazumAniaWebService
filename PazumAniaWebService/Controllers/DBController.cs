using PazumAniaWebService.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PazumAniaWebService.Controllers
{
    public class DBController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        // GET User
        [HttpGet]
        public ActionResult Login()
        {
            Users users = new Users
            {
                Id = 1,
                Login = "Thierry",
                Password = "Bizon"
            };

            return View();
        }
        void connectrionString()
        {
            con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UsersContext-20200103151859;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        public ActionResult Verify(string login,string password )
        {
            connectrionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from Users where Login='" + login + "' and Password='"+ password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                ViewBag.Message = "Well done, you have successfully logged in /n Your credentials are ok";
                return View();
            }
            else
            {
                ViewBag.Message = "Im sorry you have not logged successfully /n Check your credentials";
                con.Close();
                return View();
            }
        }
    }
}
