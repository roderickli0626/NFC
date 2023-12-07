using NFC.DAO;
using NFC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFC.Controller
{
    public class UserController
    {
        private UserDAO userDAO;
        public UserController() 
        {
            userDAO = new UserDAO();
        }

        public SearchResult Search(int start, int length, string searchVal, int type)
        {
            SearchResult result = new SearchResult();
            IEnumerable<User> userList = userDAO.FindAll();
            if (!string.IsNullOrEmpty(searchVal)) userList = userList.Where(x => x.UserName.ToLower().Contains(searchVal.ToLower())).ToList();
            if (type != 0) userList = userList.Where(x => x.TypeOfTag == type).ToList();

            result.TotalCount = userList.Count();
            userList = userList.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (User user in userList)
            {
                UserCheck userCheck = new UserCheck(user);
                checks.Add(userCheck);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeleteUser(int id)
        {
            User item = userDAO.FindByID(id);
            if (item == null) return false;

            return userDAO.Delete(id);
        }

        public bool EnableUser(int id)
        {
            User item = userDAO.FindByID(id);
            if (item == null) return false;

            item.IsEnabled = !(item.IsEnabled ?? false);

            return userDAO.Update(item);
        }

        public bool SaveUser(int? userID, string name, string surname, string email, string address, string city, string phone, string mobile, int? type, string UID, string note)
        {
            User user = userDAO.FindByID(userID ?? 0);
            if (user == null)
            {
                user = new User();
                user.UserName = name;
                user.Surname = surname;
                user.Email = email;
                user.Address = address;
                user.City = city;
                user.Phone = phone;
                user.Mobile = mobile;
                user.TypeOfTag = type;
                user.UID = UID;
                user.Note = note;
                user.IsEnabled = true;

                return userDAO.Insert(user);
            }
            else
            {
                user.UserName = name;
                user.Surname = surname;
                user.Email = email;
                user.Address = address;
                user.City = city;
                user.Phone = phone;
                user.Mobile = mobile;
                user.TypeOfTag = type;
                user.UID = UID;
                user.Note = note;

                return userDAO.Update(user);
            }
        }
    }
}