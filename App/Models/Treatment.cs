using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class Treatment
    {
        [Required]
        public int TreatmentId { get; set; }

        public string Name { get; set; }
        public string Availability { get; set; }

        public string Day { get; set; }
        public DateTime Date { get; set; }
        public string Specification { get; set; }  
        
        public double Price { get; set; }
        public Guid BusinessId { get; set; }
        public string DoctorName { get; set; } // name of doctor
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
      
        public List<Appointment> appointments { get; set; }

        public Treatment() { }

       
    }
}
