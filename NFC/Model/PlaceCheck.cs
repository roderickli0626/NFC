using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFC.Model
{
    public class PlaceCheck
    {
        Place place;
        public PlaceCheck() { }
        public PlaceCheck(Place place)
        {
            this.place = place;
            if (place == null) return;
            Id = place.Id;
            Title = place.PlaceTitle;
            IPAddress = place.IPAddress;
            Note = place.Note;
        }

        public int Id
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }
        public string IPAddress
        {
            get; set;
        }
        public string Note
        {
            get; set;
        }
    }
}