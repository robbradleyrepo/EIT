namespace LionTrust.Foundation.SitecoreExtensions.Extensions
{
    using Sitecore.Globalization;
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

        public static string ToDateTimeString(this DateTime dateTime)
        {
            var label = string.Empty;
            if (dateTime.Date == DateTime.Today)
            {
                label = $"{Translate.Text("Today")} {dateTime:hh:mm tt}";
            }
            else if (DateTime.Today - dateTime.Date == TimeSpan.FromDays(1))
            {
                label = $"{Translate.Text("Yesterday")} {dateTime:hh:mm tt}";
            }

            if (string.IsNullOrEmpty(label))
            {
                label = dateTime.ToString("f", Sitecore.Context.Culture);
            }

            return label;
        }

        public static string ToDateTimeWithTicks(this DateTime dateTime)
        {
            return $"{dateTime:yyyyMMddTHHmmss}:{dateTime.Ticks}Z";
        }
    }
}