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
            AdminID = accessLog.AdminID;
            if (accessLog.Admin != null)
            {
                UserName = accessLog.Admin.Name;
                UID = accessLog.Admin.UID;
                AccessType = accessLog.Admin.TypeOfTag ?? 0;
            }
            else
            {
                UserName = accessLog.User?.UserName ?? "";
                SurName = accessLog.User?.Surname ?? "";
                UID = accessLog.User?.UID ?? "";
                AccessType = accessLog.User?.TypeOfTag ?? 0;
            }
            PlaceID = accessLog.PlaceID;
            PlaceTitle = accessLog.Place?.PlaceTitle ?? "";
            AccessDate = accessLog.AccessDate?.ToString("dd/MM/yyyy HH.mm");
            Note = accessLog.Note;
            IsIn = accessLog.IsIn ?? false;
            IsOut = accessLog.IsOut ?? false;
            
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
        public int? AdminID
        {
            get; set;
        }
        public string UserName
        {
            get; set;
        }
        public string SurName
        {
            get; set;
        }
        public int? PlaceID
        {
            get; set;
        }
        public string PlaceTitle
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
        public string UID
        {
            get; set;
        }
        public bool IsIn
        {
            get; set;
        }
        public bool IsOut
        {
            get; set;
        }
        public int AccessType
        {
            get; set;
        }
    }
}