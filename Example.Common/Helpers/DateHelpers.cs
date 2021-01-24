using System;

namespace Example.Common.Helpers
{
    public static class DateHelpers
    {
        /// <summary>
        /// Tarihi günün ilk saatine ayarlar.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime SetStartHour(this DateTime date)
        {
            try
            {
                return new DateTime(date.Year, date.Month, date.Day);
            }
            catch
            {
                return date;
            }
        }

        /// <summary>
        /// Tarihi günün son saatine ayarlar.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime SetEndHour(this DateTime date)
        {
            try
            {
                return new DateTime(date.Year, date.Month, date.Day + 1).AddMilliseconds(-1);
            }
            catch
            {
                return date;
            }
        }

        /// <summary>
        /// Tarihi bulunduğu ayın ilk gününe ayarlar.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime SetStartOfTheMonth(this DateTime date)
        {
            try
            {
                return new DateTime(date.Year, date.Month , 1);
            }
            catch
            {
                return date;
            }
        }

        /// <summary>
        /// Tarihi bulunduğu ayın son gününe ayarlar.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime SetEndOfTheMonth(this DateTime date)
        {
            try
            {
                return new DateTime(date.Year, date.Month + 1, 1).AddMilliseconds(-1);
            }
            catch
            {
                return date;
            }
        }

        /// <summary>
        /// Tarih hafta sonuna geliyorsa önceki cuma gününe ayarlar.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime SetWeekendPreviousFriday(DateTime date)
        {
            try
            {
                return date.DayOfWeek switch
                {
                    DayOfWeek.Saturday => date.AddDays(-1),
                    DayOfWeek.Sunday => date.AddDays(-2),
                    _ => date
                };
            }
            catch
            {
                return date;
            }
        }

        /// <summary>
        /// Tarih hafta sonuna geliyorsa sonraki pazartesi gününe ayarlar.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime SetWeekendNextMonday(this DateTime date)
        {
            try
            {
                return date.DayOfWeek switch
                {
                    DayOfWeek.Saturday => date.AddDays(2),
                    DayOfWeek.Sunday => date.AddDays(1),
                    _ => date
                };
            }
            catch
            {
                return date;
            }
        }
    }
}
