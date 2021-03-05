using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModel
{
    public class TimeSpanView
    {
        [Required]
        public int Hours { get; set; }
        [Required]
        public int Minutes { get; set; }
        [Required]
        public int Seconds { get; set; }
    }
}
