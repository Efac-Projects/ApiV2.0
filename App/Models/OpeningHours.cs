using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class OpeningHour
    {
        public int Id { get; set; }
        public IList<WorkDay> WorkDays { get; set; } // each day of week and opening hour 

        public OpeningHour()
        {
            WorkDays = new List<WorkDay>();
            //FillHours();
        }

        //public OpeningHours(IList<WorkDay> workDays)
        //{

        //    FillHours();
        //}

        public void EditHoursOfDay(DayOfWeek day, List<TimeRange> hours)
        {
            this.WorkDays.Single(wd => wd.Day == day).Hours = hours;
        }

        public void FillHours()
        {
            var days = Enum.GetValues(typeof(DayOfWeek));

            foreach (DayOfWeek day in days)
                WorkDays.Add(new WorkDay(day, new List<TimeRange>()));
        }
    }
}
