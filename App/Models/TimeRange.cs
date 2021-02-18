using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class TimeRange : ICloneable
    {
        public int Id { get; set; }
        [Required]
        public Time StartTime { get; set; }
        [Required]
        public Time EndTime { get; set; }

        protected TimeRange() { }

        public TimeRange(Time startTime, Time endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public object Clone()
        {
            return new TimeRange(StartTime, EndTime);
        }
    }
}
