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
        public IList<AppointmentTreatment> Treatments { get; set; }
        public DateTime StartMoment { get; set; }
        public DateTime EndMoment => StartMoment.Add(TotalDuration);

        // find total duration for all appointments
        public TimeSpan TotalDuration
        {
            get
            {
                TimeSpan totalDuration = new TimeSpan();
                foreach (AppointmentTreatment tr in Treatments) totalDuration = totalDuration.Add(tr.Treatment.Duration);
                return totalDuration;
            }
        }

        

        protected Appointment() { }

        public Appointment(IList<Treatment> treatments, DateTime startMoment, string firstname, string lastname)
        {
            Treatments = new List<AppointmentTreatment>();
            foreach (Treatment tr in treatments) Treatments.Add(new AppointmentTreatment(this, tr));

            StartMoment = startMoment;
            FirstName = firstname;
            LastName = lastname;
        }





    }
}
