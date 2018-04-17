using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mohdthat.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Mohdthat.Models
{
    public class User_Contacts
    {
        public int Id { get; set; }

        public string CurrnetUser { get; set; }
        public DateTime CreatedAt { get; set; }

        public string UserSelected { get; set; }

        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}