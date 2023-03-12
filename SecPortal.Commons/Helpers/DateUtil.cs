using System;
using System.Globalization;

namespace SecPortal.Common.Helpers
{
    public static class DateUtil
    {
        public static string ApplicationDateFormat = "dd-MM-yyyy";

        public static DateTime Now => DateTime.Now;

        public static DateTime ParseStringToDateTime(string dateString, string format = "")
        {
            if (string.IsNullOrEmpty(format))
            {
                format = ApplicationDateFormat;
            }
            return DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
        }

        public static string ParseDateToString(DateTime targetDate, string format = "")
        {
            if (string.IsNullOrEmpty(format))
            {
                format = ApplicationDateFormat;
            }

            return targetDate.ToString(format);
        }

        public static DateTime ToDateTime(string targetDate)
        {
            return DateTime.ParseExact(targetDate, DateUtil.ApplicationDateFormat, CultureInfo.InvariantCulture);
        }

        public new static string ToString()
        {
            return Now.ToString(ApplicationDateFormat);
        }

        public static string ToString(DateTime targetDate)
        {
            return targetDate.ToString(ApplicationDateFormat);
        }

        public static string ToString(DateTime? targetDate)
        {
            if (targetDate == null)
            {
                return ToString();
            }
            else
            {
                return ToString((DateTime)targetDate);
            }
        }

    }
}
