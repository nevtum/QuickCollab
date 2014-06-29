using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuickCollab.Models
{
    public class SessionStartSettings
    {
        public SessionStartSettings()
        {
            IsVisible = true;
        }

        [Required(ErrorMessage="Please enter a username")]
        [MaxLength(40)]
        public string UserName { get; set; }

        [Required(ErrorMessage="Please enter a session name")]
        [MaxLength(50)]
        public string SessionName { get; set; }

        public bool IsVisible { get; set; }

        public bool WithPassword { get; set; }

        [MinLength(8)]
        public string SessionPassword { get; set; }
    }
}