using System;

namespace SecPortal.Common.Helpers
{
    public static class FormatHelper
    {
        public static string FormatDateTime(DateTime? target, string format = "dd MMM yyyy HH:mm")
        {
            if (target == null)
            {
                return "-";
            }

            return ((DateTime)target).ToString(format);
        }
        public static string FormatNumber(decimal target, string format = "N0") => target.ToString(format);
        public static string FormatNumber(double target, string format = "N0") => target.ToString(format);
        public static string FormatNumber(int target, string format = "N0") => target.ToString(format);
    }
}
