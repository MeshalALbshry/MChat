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

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public ApplicationUser Sender { get; set; }

        public Room Room { get; set; }
    }
}