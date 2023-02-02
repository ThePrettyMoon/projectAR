using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Insight
{
    public class Time
    {
        public static double GetAbsoluteTime() {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }

        public static DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        public static DateTime GetDateTimeByMillisecond( string milliseconds )
        {
            double sec;
            if (double.TryParse(milliseconds, out sec)) {
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                return startTime.AddSeconds(sec);
            }
            return DateTime.Now;
        }

        public static double GetSecondByDateTime()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
    }
}


