using System;

namespace D1.Model.Extentions
{
    public static class DateTimeExtentions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long CovertToTimestamp(this DateTime dateTime)
        {
            TimeSpan totalTime = dateTime - Epoch;
            return (long)totalTime.TotalSeconds;
        }

        public static DateTime ConvertTimeStampToDateTime(this long timeStamp)
        {
            DateTime dateTime = Epoch.AddSeconds(timeStamp);
            return dateTime;
        }
    }
}
