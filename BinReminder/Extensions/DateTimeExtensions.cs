using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Extensions
{
    public static class DateTimeExtensions
    {
        public static string FullDateWithDayAndSuffix(this DateTime date)
        {
            return $"{date.DayOfWeek} the {date.FullDateWithSuffix()}";
        }
        public static string FullDateWithSuffix(this DateTime date)
        {
            return $"{GetDaySuffix(date.Day)} of {date.ToString("MMMM")}"; 
        }

        private static string GetDaySuffix(int day)
        {
            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return $"{day}st";
                case 2:
                case 22:
                    return $"{day}nd";
                case 3:
                case 23:
                    return $"{day}rd";
                default:
                    return $"{day}th";
            }
        }
    }
}
