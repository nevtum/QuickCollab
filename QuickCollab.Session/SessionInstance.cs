using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCollab.Session
{
    public class SessionInstance
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public bool IsVisible { get; set; }
    }
}
