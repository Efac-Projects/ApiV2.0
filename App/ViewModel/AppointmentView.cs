using App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModel
{
    public class AppointmentView
    {
        public int AppointmentId { get; set; }

        public int BusinessId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public DateTime StartMoment { get; set; }

        public DateTime CreatedAt = DateTime.Now;
        public string TimeZone = TimeZoneInfo.Local.DisplayName;
        public string PhoneNumber { get; set; }

        public int TreatmentId { get; set; }




    }
}
