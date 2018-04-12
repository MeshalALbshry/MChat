using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Mohdthat.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace Mohdthat.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }


        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult AddContactView()
        {
            var users = db.Users.ToList();

            return View(users);
        }

        
        //Add Contact in user_contact table
        //public ActionResult AddContact(int userId)
        //{
        //    db.UserContacts.Add(new User_Contacts
        //    {
        //        current_user = User.Identity.Name,
        //        user_added = userId,
        //        CreatedAt = DateTime.Now
        //    });
        //    db.SaveChanges();
        //    return RedirectToAction("Home");
        //}
    }
}