using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace NFC.Util
{
    public class ParseUtil
    {
        public static int? TryParseInt(string text)
        {
            if (text == null || text.Length == 0) return null;
            string temp = text.Replace("$", "").Replace(" ", "").Replace(",", "");
            int value = 0;
            if (!int.TryParse(temp, out value)) return null;
            return value;
        }
        public static double? TryParseDouble(string text)
        {
            if (text == null || text.Length == 0) return null;
            string temp = text.Replace("$", "").Replace(" ", "").Replace(",", "");
            double value = 0;
            if (!double.TryParse(temp, out value)) return null;
            return value;
        }
        public static float? TryParseFloat(string text)
        {
            if (text == null || text.Length == 0) return null;
            string temp = text.Replace("$", "").Replace(" ", "").Replace(",", "");
            float value = 0;
            if (!float.TryParse(temp, out value)) return null;
            return value;
        }
        public static decimal? TryParseDecimal(string text, int round = 0)
        {
            if (text == null || text.Length == 0) return null;
            string temp = text.Replace("$", "").Replace(" ", "").Replace(",", "");
            decimal value = 0;
            if (!decimal.TryParse(temp, out value)) return null;

            if (round != 0)
            {
                value = Math.Round(value, 4);
            }

            return value;
        }

        public static bool? TryParseBool(string text)
        {
            if (text == null || text.Length == 0) return null;
            bool value = false;
            if (bool.TryParse(text, out value)) return value;

            string str = text.ToLower().Trim();
            if (str == "true" || str == "yes" || (TryParseInt(str) ?? 0) > 0) return true;
            if (str == "false" || str == "no" || str == "0") return false;

            return null;
        }


        public static DateTime? TryParseDate(string text, string format = "dd/MM/yyyy")
        {
            if (text == null || text.Length == 0) return null;

            DateTime dt;
            if (!DateTime.TryParseExact(text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                if (!DateTime.TryParse(text, out dt))
                    return (DateTime?)null;
            }
            DateTime rngMin = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            DateTime rngMax = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
            if (dt.CompareTo(rngMin) < 0 || dt.CompareTo(rngMax) > 0)
                return null;
            return dt;
        }

        public static List<int> ToIntArray(string str, string seps = ",", int? defval = 0)
        {
            return (str ?? "").Split(seps.ToCharArray()).Select(u => TryParseInt(u) ?? defval)
                .Where(u => u != null).Select(u => u.Value).ToList();
        }

        public static String GetEmailDomain(string address)
        {
            try
            {
                return new MailAddress(address).Host;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}