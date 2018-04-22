using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mohdthat.Models
{
    public class Room
    {
        public int Id { get; set; }


        public string Name { get; set; }


        public bool IsActive { get; set; }


        public DateTime CreatedAt { get; set; }


        //public DateTime UpdatedAt { get; set; }

        public string CreatedBy { get; set; }
    }
}