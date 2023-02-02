using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Insight
{
    public static class DateTimeExtension
    {
        public static int sec(this DateTime dateTime) {
            return dateTime.Second;
        }

        public static int min(this DateTime dateTime)
        {
            return dateTime.Minute;
        }

        public static int hour(this DateTime dateTime)
        {
            return dateTime.Hour;
        }

        public static int mday(this DateTime dateTime)
        {
            return dateTime.Day;
        }
        public static int mon(this DateTime dateTime)
        {
            return dateTime.Month;
        }
        public static int year(this DateTime dateTime)
        {
            return dateTime.Year;
        }
        public static int wday(this DateTime dateTime)
        {
            return (int)dateTime.DayOfWeek;
        }
        public static int yday(this DateTime dateTime)
        {
            return dateTime.DayOfYear;
        }
    }
}

