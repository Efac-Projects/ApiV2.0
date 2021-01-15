using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public int UserId { get; set; }
        public int BusinessId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndTime { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Approved { get; set; }
        public int ReviewId { get; set; }

        public Business Business { get; set; }
        //public Review Review { get; set; }
    }
}
