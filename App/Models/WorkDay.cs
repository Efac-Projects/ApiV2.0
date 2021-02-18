using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class WorkDay
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public IList<TimeRange> Hours { get; set; }

        protected WorkDay() { }

        public WorkDay(DayOfWeek day, IList<TimeRange> hours)
        {
            Day = day;
            Hours = hours;
        }
    }
}
