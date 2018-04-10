using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Mohdthat.Models;

namespace Mohdthat.Models
{
    public class PrivateChatConversation
    {
        public int Id { get; set; }

        public string SesstionID { get; set; }

        public string UserID1 { get; set; }

        public string UserID2 { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}