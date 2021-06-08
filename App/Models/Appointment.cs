using App.ViewModel;
using AspNetIdentityDemo.Api.Models;
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
       
        

        public int AppointmentId { get; set; }

        public Guid BusinessId { get; set; }
        public string FirstName { get; set; }  // Use this first name in notification
        public string LastName { get; set; }

        public int Age { get; set; }
        public string Gender { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public bool IsConfirmed { get; set; }

        [ForeignKey("TreatmentId")]
        public Treatment Treatment { get; set; }
        public string StartDate { get; set; } // date of appoinment
        public DateTime StartMoment { get; set; }  // time of appoinment
        public DateTime EndMoment => (StartMoment); // end time of appoinmet - tbd
        public DateTime CreatedAt { get; set; } // creation time of appoinmet, automatically assign the time


        public Appointment() { }

       public void ConfirmApp()
        {
            IsConfirmed = true;
        }



        




    }
}
