using App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModel
{
    public class AppointmentView
    {
        public int AppointmentId { get; set; }

        public int BusinessId { get; set; }
        [Required]
        public string Firstname { get; set; }


        [Required]
        public string Lastname { get; set; }
        [Required]
        public DateTime StartMoment { get; set; }
        [Required]
        public int  TreatmentId { get; set; }
    }
}
