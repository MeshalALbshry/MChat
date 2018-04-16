using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mohdthat.Models;

namespace Mohdthat.ViewModels
{
    public class UserYouAddedViewModel
    {
        public IEnumerable<User_Contacts> UserContact { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public string CurrentUser { get; set; }

    }
}