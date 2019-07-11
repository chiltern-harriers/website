using System;

namespace Harriers.Web.Extensions
{
    public static class DateExtensions
    {
        public static string ToDisplayDate(this DateTime value)
        {
            if (value != DateTime.MinValue)
            {
                return value.ToString("ddd, MMM dd, yyyy");
            }
            return string.Empty;
        }
    }
}