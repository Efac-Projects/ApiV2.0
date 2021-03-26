using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models    // edited now have to change database view
{
    public class Business
    {
        public Guid BusinessId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int TotalCrowd { get; set; }

        [DefaultValue(5)]
        public int CurrentCrowd { get; set; }

        public int PhoneNumber { get; set; } // from business sign up
        public int PostalCode { get; set; } // from business sign up

        public string BusinessType { get; set; }
        public string Summary { get; set; }
        public IList<Appointment> Appointments { get; set; }
        public IList<Treatment> Treatments { get; set; } // looks like doctors in medical centre

        private TimeSpan _maxTimeBetweenAppointments = new TimeSpan(0, 15, 0);
        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public string ImageSrc { get; set; }

        



        public Business() { }

     

       

      
        

      
       

        private bool OverlappingWithAppointment(DateTime startMoment, DateTime endMoment)
        {
            foreach (Appointment a in Appointments)
            {
                if (!(startMoment <= a.EndMoment && endMoment <= a.StartMoment))
                    if (!(startMoment >= a.EndMoment && endMoment >= a.StartMoment))
                        return true;
            }

            return false;
        }





    }
}
