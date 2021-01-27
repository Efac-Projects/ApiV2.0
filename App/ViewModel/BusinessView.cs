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
        public string Summary { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
