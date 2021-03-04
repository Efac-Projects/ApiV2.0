using App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModel
{
    public class WorkDayView
    {
        public WorkDayView()
        {

        }

        public WorkDayView(int dayId, List<TimeRange> hours)
        {
            DayId = dayId;
            Hours = hours;
        }


        [Required]
        public int DayId { get; set; }
        [Required]
        public List<TimeRange> Hours { get; set; }
    }
}
