using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace NFC.Util
{
    public class DateUtil
    {
        public static DateTime startOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        public static DateTime endOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        public static DateTime startOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0);
        }

        public static DateTime endOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999);
        }

        public static DateTime startOfQuarter(DateTime date)
        {
            int month = date.Month - (date.Month - 1) % 3;

            return new DateTime(date.Year, month, 1, 0, 0, 0, 0);
        }

        public static DateTime endOfQuarter(DateTime date)
        {
            int month = date.Month - (date.Month - 1) % 3 + 2;

            return new DateTime(date.Year, month, DateTime.DaysInMonth(date.Year, month), 23, 59, 59, 999);
        }

        public static DateTime startOfYear(DateTime date)
        {
            return new DateTime(date.Year, 1, 1, 0, 0, 0, 0);
        }

        public static DateTime endOfYear(DateTime date)
        {
            return new DateTime(date.Year, 12, DateTime.DaysInMonth(date.Year, 12), 23, 59, 59, 999);
        }

        public static DateTime startOfWeek(DateTime date)
        {
            DateTime monday = date;
            while (monday.DayOfWeek != DayOfWeek.Monday)
                monday = monday.AddDays(-1);
            return monday;
        }
        public static DateTime endOfWeek(DateTime date)
        {
            DateTime monday = startOfWeek(date);
            return endOfDay(monday.AddDays(6));
        }


        public static DateTime? TryParseDate(string text, string format = "dd/MM/yyyy")
        {
            if (text == null || text.Length == 0) return null;

            DateTime dt;
            if (!DateTime.TryParseExact(text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                try
                {
                    dt = DateTime.Parse(text);
                }
                catch (Exception) { return null; }
            }
            return dt;
        }


        public static DateTime AddWeeks(DateTime dateTime, int numberOfWeeks)
        {
            return dateTime.AddDays(numberOfWeeks * 7);
        }


        public static Tuple<DateTime, DateTime> getDateRangeByTypeString(string filter)
        {
            DateTime now = DateTime.Now;
            DateTime start, end;
            if (filter == "cm")
            {
                start = startOfMonth(now);
                end = endOfMonth(now);
            }
            else if (filter == "lm")
            {
                start = startOfMonth(now.AddMonths(-1));
                end = endOfMonth(now.AddMonths(-1));
            }
            else if (filter == "cq")
            {
                start = startOfQuarter(now);
                end = endOfQuarter(now);
            }
            else if (filter == "lq")
            {
                start = startOfQuarter(now.AddMonths(-3));
                end = endOfQuarter(now.AddMonths(-3));
            }
            else if (filter == "cfy")
            {
                if (now.Month <= 5)                  // Months are from 0 to 11
                {
                    start = new DateTime(now.Year - 1, 6, 1);
                    end = new DateTime(now.Year, 5, 30);
                }
                else
                {
                    start = new DateTime(now.Year, 6, 1);
                    end = new DateTime(now.Year + 1, 5, 30);
                }
            }
            else if (filter == "lfy")
            {
                if (now.Month <= 5)                  // Months are from 0 to 11
                {
                    start = new DateTime(now.Year - 2, 6, 1);
                    end = new DateTime(now.Year - 1, 5, 30);
                }
                else
                {
                    start = new DateTime(now.Year - 1, 6, 1);
                    end = new DateTime(now.Year, 5, 30);
                }
            }
            else
            {
                start = now.AddYears(-1);
                end = now;
            }
            return new Tuple<DateTime, DateTime>(start, end);
        }
        public static DateTime AddBusinessDays(DateTime date, int days)
        {
            if (days == 0) return date;
            int inc = days > 0 ? 1 : -1;
            while (days != 0)
            {
                do
                {
                    date = date.AddDays(inc);
                } while (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday);

                days -= inc;
            }
            return date;
        }
    }
}