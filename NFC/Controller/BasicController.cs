using NFC.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFC.Controller
{
    public class BasicController
    {
        private AccessLogDAO accessLogDAO;
        private PlaceDAO placeDAO;
        private PlaceAccessDAO placeAccessDAO;
        public BasicController() 
        { 
            accessLogDAO = new AccessLogDAO();
            placeDAO = new PlaceDAO();
            placeAccessDAO = new PlaceAccessDAO();
        }

    }
}