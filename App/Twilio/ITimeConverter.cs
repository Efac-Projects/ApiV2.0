using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Twilio
{
    public interface ITimeConverter
    {
        DateTime ToLocalTime(DateTime time, string timezone);
    }

    public class TimeConverter : ITimeConverter
    {
        public DateTime ToLocalTime(DateTime time, string timezone)
        {
            return TimeZoneInfo.ConvertTimeToUtc(
                time,
                TimeZoneInfo.FindSystemTimeZoneById(timezone))
                .ToLocalTime();
        }
    }
}
