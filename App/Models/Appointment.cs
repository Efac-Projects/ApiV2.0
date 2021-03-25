using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class Appointment
    {
        public static int ReminderTime = 30;
        public int AppointmentId { get; set; }

        public int BusinessId { get; set; }
        public string FirstName { get; set; }  // Use this first name in notification
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
        public string Timezone { get; set; }
        
        [ForeignKey("TreatmentId")]
        public int ThreatmentId { get; set; }
        public DateTime StartMoment { get; set; }  // time in sms sender 
        public DateTime EndMoment => StartMoment;
        public DateTime CreatedAt { get; set; }




        // find total duration for one appointment, this should changed according to requirements
        // public TimeSpan TotalDuration
        // {
        //get
        //{
        //TimeSpan totalDuration = new TimeSpan();
        //foreach (AppointmentTreatment tr in Treatments) totalDuration = totalDuration.Add(tr.Treatment.Duration);
        //totalDuration = Threatment.Duration;
        //return totalDuration;


        //}
        // }



        public Appointment() { }



        




    }
}
