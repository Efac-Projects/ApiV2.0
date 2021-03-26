using App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModel
{
    public class TreatmentView
    {
        public int Id { get; set; }

        public Guid BusinessId { get; set; }
        public string Specification { get; set; } // specification - to be 
        
        // format(hour,minutues,seconds)
        public TimeSpan Duration { get; set; } // 
        
        // MEN,WOMEN,CHILDREN
        public TreatmentCategory Category { get; set; }

       

        // maximum appoinment for threatment
        
        public double Price { get; set; }

        public string DoctorName { get; set; }
    }
}
