using NFC.DAO;
using NFC.Model;
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

        public SearchResult SearchLog(int start, int length, string searchVal, int type, DateTime? from, DateTime? to)
        {
            SearchResult result = new SearchResult();
            IEnumerable<AccessLog> logList = accessLogDAO.FindAll().OrderByDescending(m => m.AccessDate);
            logList = logList.Where(m => m.User.UserName.ToLower().Contains(searchVal.ToLower()));
            if (type != 0) logList = logList.Where(l => l.User.TypeOfTag == type);

            if (from != null)
                logList = logList.Where(u => u.AccessDate >= from.Value);

            if (to != null)
                logList = logList.Where(u => u.AccessDate <= to.Value);

            result.TotalCount = logList.Count();
            logList = logList.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (AccessLog log in logList)
            {
                AccessLogCheck accessLogCheck = new AccessLogCheck(log);
                checks.Add(accessLogCheck);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeleteAccessLog(int id)
        {
            AccessLog log = accessLogDAO.FindByID(id);
            if (log == null) return false;

            return accessLogDAO.Delete(id);
        }

        public SearchResult SearchPlaces(int start, int length, int userID)
        {
            SearchResult result = new SearchResult();
            IEnumerable<PlaceAccess> placeList = placeAccessDAO.FindAll().OrderByDescending(m => m.PlaceID);
            placeList = placeList.Where(place => place.UserID == userID);

            result.TotalCount = placeList.Count();
            placeList = placeList.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (PlaceAccess place in placeList)
            {
                PlaceAccessCheck placeCheck = new PlaceAccessCheck(place);
                checks.Add(placeCheck);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeletePlace(int id)
        {
            PlaceAccess place = placeAccessDAO.FindByID(id);
            if (place == null) return false;

            return placeAccessDAO.Delete(id);
        }

        public bool SavePlace(int userID, int placeID, DateTime? expireDate, string note) 
        {
            PlaceAccess place = placeAccessDAO.FindByUserID(userID).Where(p => p.PlaceID == placeID).FirstOrDefault();
            if (place == null)
            {
                place = new PlaceAccess();
                place.PlaceID = placeID;
                place.Note = note;
                place.UserID = userID;
                place.ExpireDate = expireDate;
                return placeAccessDAO.Insert(place);
            }
            else
            {
                place.ExpireDate = expireDate;
                place.Note = note;
                return placeAccessDAO.Update(place);
            }
        }
    }
}