using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Mohdthat.Models;

namespace Mohdthat.Models
{
    public class User_Room
    {
        public int Id { get; set; }

        public string UserID { get; set; }
        public ApplicationUser User { get; set; }

        public int RoomID { get; set; }
        public Room Room { get; set; }

        public string UserSelected { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}