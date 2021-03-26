using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class NotificationBar
    {
        [Key]
        public int NotificationId { get; set; }
        [DefaultValue("08:00")]
        public DateTime OpeningHour { get; set; }
        [DefaultValue("20:00")]
        public DateTime ClosingHour { get; set; }
        public Guid BusinessId { get; set; }

        [DefaultValue("We are Open Today")]
        public string Notification { get; set; }
    }
}
