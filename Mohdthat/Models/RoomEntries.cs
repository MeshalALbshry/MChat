using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mohdthat.Models
{
    public class RoomEntries
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        public ApplicationUser Sender { get; set; }
        public string SenderID { get; set; }

        public Room Room { get; set; }
    }
}