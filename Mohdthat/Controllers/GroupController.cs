using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Mohdthat.Models;
using Mohdthat.ViewModels;

namespace Mohdthat.Controllers
{
    public class GroupController : Controller
    {

        private ApplicationDbContext db;

        public GroupController()
        {
            db = new ApplicationDbContext();
        }
        // GET: /Group/
        public ActionResult Index()
        {
            var GroupsAll = db.Room.ToList();
            var Groups = new RoomsViewModel
            {
                Room = new Room(),
                Rooms = GroupsAll
            };

            return View(Groups);
        }

        //Add Room
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Room room)
        {
            var currentUser = User.Identity.Name;
            var cUser = db.Users.SingleOrDefault(u => u.UserName == currentUser);
            if (!ModelState.IsValid)
                return View("Index", "Home");

            if (room.Id == 0)
            {
                db.Room.Add(new Room { 
                    Name = room.Name,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    CreatedBy = currentUser,
                });
                db.SaveChanges();
                var iRoom = db.Room.FirstOrDefault(i => i.Name == room.Name);
                if (iRoom != null)
                {
                    db.UserRoom.Add(new User_Room
                    {
                        User = cUser,
                        UserSelected = cUser.UserName,
                        RoomID = iRoom.Id,
                        CreatedAt = DateTime.Now
                    });
                    db.SaveChanges();
                }  
            }
            else
            {
                var roomInDB = db.Room.Single(r => r.Id == room.Id);
                roomInDB.Name = room.Name;
                roomInDB.IsActive = true;
                roomInDB.CreatedAt = DateTime.Now;
                db.SaveChanges();
            }
            
            return RedirectToAction("Index","Group");
        }

        public ActionResult AddMembersView(int id)
        {
            var group = db.Room.FirstOrDefault(g => g.Id == id);
            //To add members to group
            var currnetUser = User.Identity.Name;
            var users = db.Users.ToList();
            var userContact = db.UserContacts.Where(u => u.CurrnetUser == currnetUser);
            //var room = db.Room.Where();
            var userRoom = db.UserRoom.ToList();
            var usersWithUserContact = new UserYouAddedViewModel
            {
                UserRoom = userRoom,
                Users = users,
                CurrentUser = currnetUser,
                Group = group
            };

            if (group == null)
                return HttpNotFound();

            return View(usersWithUserContact);
        }

        //Add Mebember to group
        public ActionResult Add(string id , int roomId)
        {
            var user_added = db.Users.SingleOrDefault(u => u.Id == id);

            if (id == null)
                return HttpNotFound();

            db.UserRoom.Add(new User_Room {
                User = user_added,
                UserSelected = user_added.UserName,
                RoomID = roomId,
                CreatedAt = DateTime.Now
            });
            db.SaveChanges();

            return RedirectToAction("Index", "Group");
        }
        public ActionResult Delete(string id)
        {
            var currnetUser = User.Identity.Name;
            var cuser_added = db.Users.FirstOrDefault(u => u.UserName == currnetUser);

            var userRoom = db.UserRoom.FirstOrDefault(u => u.UserID == id);

            if (id == null)
                return HttpNotFound();

            db.UserRoom.Remove(userRoom);
            db.SaveChanges();

            return RedirectToAction("Index", "Group");
        }

	}
}