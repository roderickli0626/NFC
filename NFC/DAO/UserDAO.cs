using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace NFC.DAO
{
    public class UserDAO : BasicDAO
    {
        public UserDAO() { }
        public List<User> FindAll()
        {
            Table<User> table = GetContext().Users;
            return table.ToList();
        }
        public User FindByID(int id)
        {
            return GetContext().Users.Where(g => g.Id == id).FirstOrDefault();
        }
        public bool Insert(User user)
        {
            GetContext().Users.InsertOnSubmit(user);
            GetContext().SubmitChanges();
            return true;
        }
        public bool Update(User user)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, user);
            return true;
        }
        public bool Delete(int id)
        {
            User user = GetContext().Users.SingleOrDefault(u => u.Id == id);
            GetContext().Users.DeleteOnSubmit(user);
            GetContext().SubmitChanges();
            return true;
        }
    }
}