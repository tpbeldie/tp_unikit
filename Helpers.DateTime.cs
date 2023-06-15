using System;

namespace tp_unikit.Helpers.Calendar
{
    public static class DateTimeHelper
    {

        private static readonly DateTime s_unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static double GetUnixTimestamp(DateTime date) {
            return Convert.ToInt64((date - s_unixEpoch).TotalSeconds);
        }

        public static double GetUnixTimestampMilliseconds(DateTime date) {
            return (date - s_unixEpoch).TotalMilliseconds;
        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp) {
            return new DateTime(s_unixEpoch.Ticks).AddSeconds(timestamp).ToLocalTime();
        }

        public static DateTime ConvertFromUnixTimestampMiliseconds(double timestamp) {
            return new DateTime(s_unixEpoch.Ticks).AddMilliseconds(timestamp).ToLocalTime();
        }

        public static DateTime MinDate(DateTime a, DateTime b) {
            return (a > b) ? b : a;
        }

        public static DateTime MaxDate(DateTime a, DateTime b) {
            return (a > b) ? a : b;
        }
    }
}
