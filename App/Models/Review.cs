using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int Appointment { get; set; }
        public string Comment { get; set; }
    }
}
