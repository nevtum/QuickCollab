using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [Display(Name = "Connection Expiry")]
        public int ConnectionExpiryInHours { get; set; }

        public IEnumerable<SelectListItem> ConnectionExpiryOptions
        {
            get
            {
                List<SelectListItem> connectionExpiryOptions = new List<SelectListItem>();
                connectionExpiryOptions.Add(new SelectListItem() { Text = "1 hour", Value = "1" });
                connectionExpiryOptions.Add(new SelectListItem() { Text = "2 hours", Value = "2" });
                connectionExpiryOptions.Add(new SelectListItem() { Text = "3 hours", Value = "3" });
                connectionExpiryOptions.Add(new SelectListItem() { Text = "12 hours", Value = "12" });
                connectionExpiryOptions.Add(new SelectListItem() { Text = "24 hours", Value = "24" });
                connectionExpiryOptions.Add(new SelectListItem() { Text = "48 hours", Value = "48" });
                connectionExpiryOptions.Add(new SelectListItem() { Text = "72 hours", Value = "72" });
                return connectionExpiryOptions;
            }
        }
    }
}