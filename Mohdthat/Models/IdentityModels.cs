using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Mohdthat.Models;

namespace Mohdthat.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomEntries> RoomEntries { get; set; }
        public DbSet<User_Room> UserRoom { get; set; }
        public DbSet<PrivateChatConversation> PrivateChatConversation { get; set; }
        public DbSet<PrivateChatEntries> PrivateChatEntries { get; set; }
        public DbSet<User_Contacts> UserContacts { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}