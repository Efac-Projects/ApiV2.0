﻿using App.Models;
using AspNetIdentityDemo.Api.Models;
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

        public Guid BusinessId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string StartDate { get; set; } // date of appoinment
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DateTime CreatedAt = DateTime.Now;
        public int Age { get; set; }
        public string Gender { get; set; }
        
        public bool IsConfirmed = false;

        public string PhoneNumber { get; set; }

        public int TreatmentId { get; set; }


    }


}
