using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public enum TreatmentCategory
    {
        MEN,
        WOMEN,
        CHILDREN
    }
    public class Treatment
    {
        [Required]
        public int TreatmentId { get; set; }
        public string Specification { get; set; } 
        public TimeSpan Duration { get; set; }
        public TreatmentCategory Category { get; set; }
        public double Price { get; set; }

        //public int MaxAppoinment { get; set; }

        public Guid BusinessId { get; set; }
        public string DoctorName { get; set; } // name of doctor
        public List<Appointment> appointments { get; set; }

        public Treatment() { }

        public Treatment(string name, TimeSpan duration, TreatmentCategory category, double price )
        {
            Specification = name;
            Duration = duration;
            Category = category;
            Price = price;
            
        }
    }
}
