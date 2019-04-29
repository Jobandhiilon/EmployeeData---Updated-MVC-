using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeData.Models;
using Microsoft.AspNetCore.Http;
using System.Data.SQLite;

namespace EmployeeData.Controllers
{
    public class HomeController : Controller
    {
        public List<string[]> display = new List<string[]> { };
        public IActionResult Index()
        {
            try
            {
                //Establishing a new SQLite Connection
                SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=data.db;Version=3;New=False;Compress=True;");
                sQLiteConnection.Open();
                SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
                sQLiteCommand.CommandText = "CREATE TABLE emp (ID varchar, Name varchar, Designation varchar, Contact varchar)";
                sQLiteCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }

            return View();
        }

        //Create Method [POST]
        [HttpPost]
        public ActionResult Create(IFormCollection formCollection)
        {
            string id = Request.Form["id"];
            string name = Request.Form["name"];
            string designation = Request.Form["designation"];
            string contact = Request.Form["contact"];

            SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=data.db;Version=3;New=False;Compress=True;");
            sQLiteConnection.Open();
            SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();

            //Giving INSERT command to add a new entry to our table
            sQLiteCommand.CommandText = "INSERT INTO emp (ID, Name, Designation, Contact) VALUES (" + "'"+id+ "'," + "'"+name+ "'," + "'"+designation+ "'," + "'"+contact+"'" + ")";
            sQLiteCommand.ExecuteNonQuery();

            return View();
           
       }

        // Read Method [GET]
        [HttpGet]
        public ActionResult Read()
        {

            SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=data.db;Version=3;New=False;Compress=True;");
            sQLiteConnection.Open();
            SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();

            //Giving INSERT command to add a new entry to our table
            sQLiteCommand.CommandText = "SELECT * FROM emp";

            SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();

            while (sQLiteDataReader.Read())
            {

                String[] line = new string[4];

                //Fetching data from the table and adding it to list
                line[0] = sQLiteDataReader.GetString(0);
                line[1] = sQLiteDataReader.GetString(1);
                line[2] = sQLiteDataReader.GetString(2);
                line[3] = sQLiteDataReader.GetString(3);

                display.Add(line);
            }

            ViewData["Display"] = display;

            return View();

        }

        // Delete Method [POST]
        [HttpPost]
        public ActionResult Delete(IFormCollection formCollection)
        {
            string id = Request.Form["id"];

            SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=data.db;Version=3;New=False;Compress=True;");
            sQLiteConnection.Open();
            SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();

            //Giving DELETE command to remove an entry from our table
            sQLiteCommand.CommandText = "DELETE FROM emp WHERE ID = " + "'" + id + "'";

            sQLiteCommand.ExecuteNonQuery();

            return View();

        }

        // Update Method
        [HttpPost]
        public ActionResult Update(IFormCollection formCollection)
        {
            string id = Request.Form["id"];
            string name = Request.Form["name"];
            string designation = Request.Form["designation"];
            string contact = Request.Form["contact"];
            SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=data.db;Version=3;New=False;Compress=True;");
            sQLiteConnection.Open();
            SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();

            //Giving DELETE command to remove an entry from our table
            sQLiteCommand.CommandText = "UPDATE emp " + "SET Name=" + "'" + name + "'," + "Designation=" + "'" + designation + "'," + "Contact=" + "'" + contact + "' " + "WHERE ID = " + "'" + id + "'";

            sQLiteCommand.ExecuteNonQuery();

            return View();

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
