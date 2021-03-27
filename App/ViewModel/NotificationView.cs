using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModel
{
    public class NotificationView
    {
        public int NotificationId { get; set; }

        public Guid BusinessId { get; set; }
        public DateTime OpeningHour { get; set; }
        public DateTime ClosingHour { get; set; }
        public string Notification { get; set; }
    }
}
