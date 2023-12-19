using NFC.DAO;
using NFC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFC.Controller
{
    public class AdminController
    {
        private AdminDAO adminDAO;
        public AdminController() 
        {
            adminDAO = new AdminDAO();
        }

        public SearchResult Search(int start, int length, string searchVal)
        {
            SearchResult result = new SearchResult();
            IEnumerable<Admin> adminList = adminDAO.FindAll();
            if (!string.IsNullOrEmpty(searchVal)) adminList = adminList.Where(x => x.Name.ToLower().Contains(searchVal.ToLower()) || x.UID.Contains(searchVal)).ToList();

            result.TotalCount = adminList.Count();
            adminList = adminList.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (Admin admin in adminList)
            {
                AdminCheck adminCheck = new AdminCheck(admin);
                checks.Add(adminCheck);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeleteAdmin(int id)
        {
            Admin item = adminDAO.FindByID(id);
            if (item == null) return false;

            return adminDAO.Delete(id);
        }

        public bool SaveAdmin(int? adminID, string name, string email, EncryptedPass pass, string note, string UID, int? type)
        {
            Admin admin = adminDAO.FindByID(adminID ?? 0);
            if (admin == null)
            {
                Admin existAdmin = adminDAO.FindByEmail(email);
                if (existAdmin != null) return false;
                admin = new Admin();
                admin.Name = name;
                admin.Email = email;
                admin.UID = UID;
                admin.TypeOfTag = type;
                admin.Note = note;
                admin.Password = pass?.Encrypted ?? "";

                return adminDAO.Insert(admin);
            }
            else
            {
                admin.Name = name;
                admin.Email = email;
                admin.UID = UID;
                admin.TypeOfTag = type;
                admin.Note = note;
                if (pass != null)
                {
                    admin.Password = pass.Encrypted;
                }
                return adminDAO.Update(admin);
            }
        }
    }
}