using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class Business
    {
        public int BusinessId { get; set; }
        public string BusinessType { get; set; }
        public string BusinessName { get; set; }
        public int TotalCrowd { get; set; }
        public int CurrentCrowd { get; set; }
        public int EmergencyAppointment { get; set; }
        public string Summary { get; set; }
        public string Address { get; set; } //  small issue
        public DateTime PublishedAt { get; set; }
        public int UserId { get; set; }
        public int PhoneNumber { get; set; } // from login
        public int PostalCode { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsApproved { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Service> Services { get; set; }

    }
}
