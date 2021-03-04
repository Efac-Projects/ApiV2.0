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
        [Required]
        public string Name { get; set; }
        [Required]
        public TimeSpanView Duration { get; set; }
        [Required]
        public TreatmentCategory Category { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
