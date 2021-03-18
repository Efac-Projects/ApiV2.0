using App.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModel
{
    public class BusinessView
    {
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public int TotalCrowd { get; set; }
        public int CurrentCrowd { get; set; }
        public int PhoneNumber { get; set; }
        public int PostalCode { get; set; }
        public string Email { get; set; }
        public string BusinessType { get; set; }
        public string Summary { get; set; }

        public IFormFile ImageFile { get; set; }


        public IList<Treatment> Treatments { get; set; }
        public WorkDayView2 WorkDays { get; set; }
    }
}
