using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public int ServiceName { get; set; }
        public int BusinessId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime  { get; set; }
        public string Summary { get; set; }
        public string ServiceOwner { get; set; }
        public bool IsAvailable { get; set; }

    }
}
