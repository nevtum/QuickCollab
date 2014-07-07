using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuickCollab.Models
{
    public class StartSettingsViewModel
    {
        public StartSettingsViewModel()
        {
            Public = true;
        }

        [Required(ErrorMessage="Please enter a session name")]
        [MaxLength(50)]
        public string SessionName { get; set; }

        public bool Public { get; set; }

        public bool PersistHistory { get; set; }

        public bool Secured { get; set; }

        [MinLength(8)]
        public string SessionPassword { get; set; }
    }
}