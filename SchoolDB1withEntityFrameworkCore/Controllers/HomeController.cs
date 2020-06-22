using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using SchoolDB1withEntityFrameworkCore.Data;
using SchoolDB1withEntityFrameworkCore.Models;

namespace SchoolDB1.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolContext dbContext;

        public HomeController(SchoolContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {


            var teachers = dbContext.Teachers.ToList();
            var models = new List<TeacherViewModel>();

            /* grab everything from the dbcontext and store it in a variable called teachers
             * then create models placeholders which are used to pass the information to the view
             * models: Teacher.Teachers()
             */

            foreach (var item in teachers)
            {
                models.Add(new TeacherViewModel()
                {
                    Id = item.Id.ToString(),
                    Name = item.Name,
                    Email = item.Email
                });
            }

            
            return View(models);
        }


        public IActionResult Create()
        {
            return View();
        }


        // C# is implicitly able to convert TeacherViewModel to teacher
        [HttpPost]
        public IActionResult Create_Post(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                dbContext.Teachers.Attach(teacher);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
                
            }
            else
                return View("~/Views/Home/Create.cshtml");
        }

        public IActionResult Edit(int id)
        {
            // Retrieve the teacher object by it's id from the database
            var teacher = dbContext.Teachers.Find(id);
            // redirect to Update Form View, pass teacher object as the model to the Update Form Update.cshtml
            return View(teacher);
        }

        [HttpPost]
        public IActionResult Edit_Post(Teacher teacher)
        {
            dbContext.Update(teacher);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }






        [HttpPost]

        public IActionResult Delete(int id)
        {
            // Retrieve the teacher object by it's id from the database
            var teacher = dbContext.Teachers.Find(id);
            // Remove the teacher object from the database using .Remove
            // Here dbContext is used as mapper ORM (Object Relational Mapper) to remove
            // dbContext establishes a connection between C# and SQL Server, it will remove 
            // the teacher object from the database
            dbContext.Remove(teacher);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
            // return View("Index"); this is wrong, here we are not passing 
            // a model to the View so we will get a null reference exception
            // instead make it call the Index method again, which will pass 
            // the correct model to the View
        }
           

    public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //   // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
