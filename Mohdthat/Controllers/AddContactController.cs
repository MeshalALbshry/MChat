using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mohdthat.Models;
using System.Linq;
using System.Data.Entity;
using Mohdthat.ViewModels;
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
            var currnetUser = User.Identity.Name;
            var users = db.Users.ToList();
            var userContact = db.UserContacts.Where(u => u.CurrnetUser == currnetUser);
            
            var usersWithUserContact = new UserYouAddedViewModel
            {
                UserContact = userContact,
                Users = users,
                CurrentUser = currnetUser
            };
            return View(usersWithUserContact);
        }


        public ActionResult Add(string id)
        {
            var currnetUser = User.Identity.Name;
            var user_added = db.Users.SingleOrDefault(u => u.Id == id);
            var cuser_added = db.Users.SingleOrDefault(u => u.UserName == currnetUser);

            if (user_added == null)
                return HttpNotFound();

            db.UserContacts.Add(new User_Contacts
            {
                CurrnetUser = currnetUser,
                CreatedAt = DateTime.Now,
                User = user_added,
                UserSelected = user_added.UserName
            });

            //To add in another user
            db.UserContacts.Add(new User_Contacts
            {
                CurrnetUser = user_added.UserName,
                CreatedAt = DateTime.Now,
                User = cuser_added,
                UserSelected = currnetUser
            });
            db.SaveChanges();
            
            return RedirectToAction("Index","AddContact");
        }
        public ActionResult Delete(string id)
        {
            var currnetUser = User.Identity.Name;
            var cuser_added = db.Users.SingleOrDefault(u => u.UserName == currnetUser);

            var userCon = db.UserContacts.FirstOrDefault(u => u.UserID == id);
            var cUserCon = db.UserContacts.FirstOrDefault(u => u.UserSelected == currnetUser);
            if (id == null)
                return HttpNotFound();

            db.UserContacts.Remove(userCon);
            db.UserContacts.Remove(cUserCon);
            db.SaveChanges();
            return RedirectToAction("Index", "AddContact");
        }


	}
}