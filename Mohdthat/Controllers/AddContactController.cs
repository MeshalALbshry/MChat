using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mohdthat.Models;

namespace Mohdthat.Controllers
{
    public class AddContactController : Controller
    {

        private ApplicationDbContext db;

        public AddContactController()
        {
            db = new ApplicationDbContext();
        }
        //
        // GET: /AddContact/
        public ActionResult Index()
        {
            var users = db.Users.ToList();

            return View(users);
        }


        public ActionResult Delete()
        {
            return View("Index");
        }


	}
}