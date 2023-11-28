using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFC.Model
{
    public class PlaceAccessCheck
    {
        PlaceAccess placeAccess;
        public PlaceAccessCheck() { }
        public PlaceAccessCheck(PlaceAccess placeAccess) 
        {
            this.placeAccess = placeAccess;
            if (placeAccess == null) { return; }
            Id = placeAccess.Id;
            PlaceID = placeAccess.PlaceID;
            UserID = placeAccess.UserID;
            PlaceTitle = placeAccess.Place?.PlaceTitle;
            UserName = placeAccess.User?.UserName;
            ExpireDate = placeAccess.ExpireDate?.ToString("dd/MM/yyyy HH.mm");
            Note = placeAccess.Note;
        }
        public int Id
        {
            get; set;
        }
        public int? PlaceID
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
        public string PlaceTitle
        {
            get; set;
        }
        public string ExpireDate
        {
            get; set;
        }
        public string Note
        {
            get; set;
        }

    }
}