﻿using System;
using System.Globalization;

namespace Stefanini.Core.Extensions
{
    public static class DateTimeExtensions
    {
#if !DEBUG
        [System.Diagnostics.DebuggerStepThrough]
#endif
        public static int GetWeekOfYear(this DateTime value)
            => CultureInfo.CurrentCulture
                          .Calendar
                          .GetWeekOfYear(value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }
}