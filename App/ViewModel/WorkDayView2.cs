using App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModel
{
    public class WorkDayView2
    {
        [Required]
        public IList<TimeRange> Monday { get; set; }
        [Required]
        public IList<TimeRange> Tuesday { get; set; }
        [Required]
        public IList<TimeRange> Wednesday { get; set; }
        [Required]
        public IList<TimeRange> Thursday { get; set; }
        [Required]
        public IList<TimeRange> Friday { get; set; }
        [Required]
        public IList<TimeRange> Saturday { get; set; }
        [Required]
        public IList<TimeRange> Sunday { get; set; }
    }
}
