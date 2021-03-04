using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class Review
    {

        public int ReviewId { get; set; }
        public string Comment { get; set; }
        public int AppointmentId { get; set; }
        public int Rating { get; set; }
        Appointment Appointment { get; set; }
    }
}
