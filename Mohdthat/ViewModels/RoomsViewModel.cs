using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mohdthat.Models;
namespace Mohdthat.ViewModels
{
    public class RoomsViewModel
    {
        public IEnumerable<Room> Rooms { get; set; }
        public Room Room { get; set; }
        public string CurrentUser { get; set; }
    }
}