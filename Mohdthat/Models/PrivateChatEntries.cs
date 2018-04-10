using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Mohdthat.Models;

namespace Mohdthat.Models
{
    public class PrivateChatEntries
    {
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        
        public string Sender { get; set; }

        public int  ConversationID { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}