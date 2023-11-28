using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFC.Model
{
    public class AccessLogCheck
    {
        AccessLog accessLog;
        public AccessLogCheck() { }
        public AccessLogCheck(AccessLog accessLog)
        {
            this.accessLog = accessLog;
            if (accessLog == null) return;
            Id = accessLog.Id;
            Detail = accessLog.AccessDetail;
            UserID = accessLog.UserID;
            UserName = accessLog.User?.UserName ?? "";
            AccessDate = accessLog.AccessDate?.ToString("dd/MM/yyyy HH.mm");
            Note = accessLog.Note;
        }
        public int Id
        {
            get; set;
        }
        public string Detail
        {
            get; set;
        }
        public int? UserID
        {
            get; set;
        }
        public string UserName
        {
            get; set;
        }
        public string AccessDate
        {
            get; set;
        }
        public string Note
        {
            get; set;
        }
    }
}