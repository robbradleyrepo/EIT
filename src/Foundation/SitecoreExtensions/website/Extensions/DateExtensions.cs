namespace LionTrust.Foundation.SitecoreExtensions.Extensions
{
    using System;

    public static class DateExtensions
    {
        public static string ToDateString(this DateTime dateTime, string dateFormat = "d MMMM yyyy", string todayString = "")
        {
            if (string.IsNullOrEmpty(todayString) 
                || !dateTime.Year.Equals(DateTime.Now.Year)
                || !dateTime.Month.Equals(DateTime.Now.Month)
                || !dateTime.Day.Equals(DateTime.Now.Day))
            {
                return dateTime.ToString(dateFormat);
            }
            else
            {
                return "Today";
            }
        }

        public static string ToDateTimeWithTicks(this DateTime dateTime)
        {
            return $"{dateTime:yyyyMMddTHHmmss}:{dateTime.Ticks}Z";
        }
    }
}