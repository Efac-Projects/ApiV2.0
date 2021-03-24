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

        public int BusinessId { get; set; }
        public string Name { get; set; } // specification - to be 
        
        public TimeSpanView Duration { get; set; } // 
        
        public TreatmentCategory Category { get; set; }

        // available time - controller for edit

        // maximum appoinment for threatment
        
        public double Price { get; set; }

        public string DoctorName { get; set; }
    }
}
