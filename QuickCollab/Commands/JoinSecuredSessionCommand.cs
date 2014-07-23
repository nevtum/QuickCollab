using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickCollab.Commands
{
    public class JoinSecuredSessionCommand
    {
        public string SessionId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}