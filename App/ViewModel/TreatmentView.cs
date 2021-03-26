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
        public string Specification { get; set; }  
  
        public string Name { get; set; }
        public string Availability { get; set; }

        public DateTime Day { get; set; }
        public DateTime Date { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public double Price { get; set; }

        public string DoctorName { get; set; }
    }
}
