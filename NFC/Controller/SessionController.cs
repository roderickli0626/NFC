using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFC.Controller
{
    public class SessionController
    {
        private static string CURRENT_USER_PASSWORD_ENCRYPTED = "CurrentUserPassword";
        private static string CURRENT_USER_ID = "CurrentUserId";
        public static string CURRENT_USER_EMAIL = "CurrentUserEmail";
        public static string SUPERADMIN = "SuperAdmin";
        public static string ADMIN = "Admin";

        public void RemoveAll()
        {
            HttpContext.Current.Session.RemoveAll();
        }
        public void SetSuperAdmin()
        {
            HttpContext.Current.Session.Add(SUPERADMIN, "true");
        }
        public void SetAdmin()
        {
            HttpContext.Current.Session.Add(ADMIN, "true");
        }

        public void SetPassword(EncryptedPass pass)
        {
            HttpContext.Current.Session.Add(CURRENT_USER_PASSWORD_ENCRYPTED, pass.Encrypted);
        }

        public void SetCurrentUserId(int userId)
        {
            HttpContext.Current.Session.Add(CURRENT_USER_ID, userId.ToString());
        }

        public void SetCurrentUserEmail(string email)
        {
            HttpContext.Current.Session.Add(CURRENT_USER_EMAIL, email);
        }

        public bool? GetSuperAdmin()
        {
            if (IsInvalidSession()) return null;
            object IsSuperAdmin = HttpContext.Current.Session[SUPERADMIN];
            if (IsSuperAdmin == null) return null;
            return IsSuperAdmin.ToString() == "true";
        }
        public bool? GetAdmin()
        {
            if (IsInvalidSession()) return null;
            object IsAdmin = HttpContext.Current.Session[ADMIN];
            if (IsAdmin == null) return null;
            return IsAdmin.ToString() == "true";
        }
        public int? GetCurrentUserId()
        {
            if (IsInvalidSession()) return null;
            object id = HttpContext.Current.Session[CURRENT_USER_ID];
            if (id == null || id.ToString() == "")
                return null;

            return int.Parse(id.ToString());
        }

        public bool IsInvalidSession()
        {
            return HttpContext.Current == null || HttpContext.Current.Session == null;
        }
        public void SetTimeout(int timeout)
        {
            HttpContext.Current.Session.Timeout = timeout;
        }

        public void LogoutUser()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }

        internal string GetPassword()
        {
            if (IsInvalidSession())
                return "";

            object ss = HttpContext.Current.Session[CURRENT_USER_PASSWORD_ENCRYPTED];
            if (ss == null)
                return "";
            string pwEnc = ss.ToString();
            if (pwEnc == "")
                return "";
            string dec = new CryptoController().DecryptStringAES(pwEnc);
            return dec;
        }

        public void SetAttribute(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public void RemoveAttribute(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public object GetAttribute(string key)
        {
            return HttpContext.Current.Session[key];
        }
    }
}