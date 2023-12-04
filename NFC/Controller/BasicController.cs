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

    }
}