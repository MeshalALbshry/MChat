using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mohdthat.Models
{
    public class User_Contacts
    {
        public int Id { get; set; }
        public string current_user { get; set; }
        public string user_added { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}