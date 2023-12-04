using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace NFC.DAO
{
    public class AdminDAO : BasicDAO
    {
        public AdminDAO() { }
        public List<Admin> FindAll()
        {
            Table<Admin> table = GetContext().Admins;
            return table.ToList();
        }
        public Admin FindByID(int id)
        {
            return GetContext().Admins.Where(g => g.Id == id).FirstOrDefault();
        }
        public Admin FindByEmail(string email)
        {
            return GetContext().Admins.Where(g => g.Email == email).FirstOrDefault();
        }
        public bool Insert(Admin admin)
        {
            GetContext().Admins.InsertOnSubmit(admin);
            GetContext().SubmitChanges();
            return true;
        }

        public bool Update(Admin admin)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, admin);
            return true;
        }
        public bool Delete(int id)
        {
            Admin admin = GetContext().Admins.SingleOrDefault(u => u.Id == id);
            GetContext().Admins.DeleteOnSubmit(admin);
            GetContext().SubmitChanges();
            return true;
        }
    }
}