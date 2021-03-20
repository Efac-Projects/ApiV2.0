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
        public string Name { get; set; }
        
        public TimeSpanView Duration { get; set; }
        
        public TreatmentCategory Category { get; set; }
        
        public double Price { get; set; }

        public string DoctorName { get; set; }
    }
}
