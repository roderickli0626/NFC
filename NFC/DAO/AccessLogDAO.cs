using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace NFC.DAO
{
    public class AccessLogDAO : BasicDAO
    {
        public AccessLogDAO() { }
        public List<AccessLog> FindAll()
        {
            Table<AccessLog> table = GetContext().AccessLogs;
            return table.ToList();
        }
        public AccessLog FindByID(int id)
        {
            return GetContext().AccessLogs.Where(g => g.Id == id).FirstOrDefault();
        }
        public List<AccessLog> FindByUserID(int userId)
        {
            return GetContext().AccessLogs.Where(g => g.UserID== userId).ToList();
        }
        public bool Insert(AccessLog log)
        {
            GetContext().AccessLogs.InsertOnSubmit(log);
            GetContext().SubmitChanges();
            return true;
        }

        public bool Update(AccessLog log)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, log);
            return true;
        }
        public bool Delete(int id)
        {
            AccessLog log = GetContext().AccessLogs.SingleOrDefault(u => u.Id == id);
            GetContext().AccessLogs.DeleteOnSubmit(log);
            GetContext().SubmitChanges();
            return true;
        }
    }
}