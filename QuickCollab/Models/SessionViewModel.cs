using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickCollab.Models
{
    public class SessionViewModel
    {
        public DateTime DateCreated { get; set; }
        public string SessionName { get; set; }
        public bool Secured { get; set; }
        public bool PersistHistory { get; set; }
        public int ConnectionExpiryInHours { get; set; }
        public bool IsVisible { get; set; }
    }
}