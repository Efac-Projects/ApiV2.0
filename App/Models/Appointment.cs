using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class Appointment
    {

        public int AppointmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Treatment Threatment { get; set; }
        public DateTime StartMoment { get; set; }
        public DateTime EndMoment => StartMoment.Add(TotalDuration);

        // find total duration for one appointment, this should changed according to requirements
        public TimeSpan TotalDuration
        {
            get
            {
                TimeSpan totalDuration = new TimeSpan();
                //foreach (AppointmentTreatment tr in Treatments) totalDuration = totalDuration.Add(tr.Treatment.Duration);
                totalDuration = Threatment.Duration;
                return totalDuration;


            }
        }



        protected Appointment() { }



        public Appointment(Treatment treatments, DateTime startMoment, string firstname, string lastname)
        {
            Threatment = treatments;
            //foreach (Treatment tr in treatments) Treatments.Add(new AppointmentTreatment(this, tr));

            StartMoment = startMoment;
            FirstName = firstname;
            LastName = lastname;
        }





    }
}
