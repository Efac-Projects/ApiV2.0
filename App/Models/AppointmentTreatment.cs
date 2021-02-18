using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.Models
{
    public class AppointmentTreatment
    {
        public int AppointmentId { get; set; }
        [JsonIgnore]
        public Appointment Appointment { get; set; }

        public int TreatmentId { get; set; }
        public Treatment Treatment { get; set; }

        protected AppointmentTreatment() { }

        public AppointmentTreatment(Appointment appointment, Treatment treatment)
        {
            Appointment = appointment;
            Treatment = treatment;
        }
    }
}
