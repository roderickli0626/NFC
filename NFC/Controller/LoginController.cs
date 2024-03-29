﻿using NFC.Common;
using NFC.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFC.Controller
{
    public class EncryptedPass
    {
        public string Encrypted { get; set; }
        public string UnEncrypted { get; set; }

    }
    public class LoginController
    {
        private AdminDAO adminDao;

        public LoginController()
        {
            adminDao = new AdminDAO();
        }

        public LoginCode LoginAndSaveSession(string email, EncryptedPass pass)
        {
            string SuperAdminEmail = System.Configuration.ConfigurationManager.AppSettings["AdminUserName"];
            string SuperAdminPass = System.Configuration.ConfigurationManager.AppSettings["AdminPassword"];

            string DoorManAdminEmail = System.Configuration.ConfigurationManager.AppSettings["DoorManUserName"];
            string DoorManAdminPass = System.Configuration.ConfigurationManager.AppSettings["DoorManPassword"];

            if (email.CompareTo(SuperAdminEmail) == 0 && pass.UnEncrypted.CompareTo(SuperAdminPass) == 0)
            {
                new SessionController().SetSuperAdmin();
                new SessionController().SetCurrentUserId(0);
                new SessionController().SetCurrentUserEmail(email);
                new SessionController().SetPassword(pass);
                new SessionController().SetTimeout(60 * 24 * 7 * 2);
                return LoginCode.Success;
            }

            if (email.CompareTo(DoorManAdminEmail) == 0 && pass.UnEncrypted.CompareTo(DoorManAdminPass) == 0)
            {
                new SessionController().SetDoorMan();
                new SessionController().SetCurrentUserId(0);
                new SessionController().SetCurrentUserEmail(email);
                new SessionController().SetPassword(pass);
                new SessionController().SetTimeout(60 * 24 * 7 * 2);
                return LoginCode.Success;
            }

            Admin admin = adminDao.FindByEmailOrName(email);
            if (admin == null) { return LoginCode.Failed; }
            string modelPW = new CryptoController().DecryptStringAES(admin.Password);
            if (pass.UnEncrypted.CompareTo(modelPW) == 0)
            {
                new SessionController().SetAdmin();
                new SessionController().SetCurrentUserId(admin.Id);
                new SessionController().SetCurrentUserEmail(admin.Email);
                new SessionController().SetPassword(pass);
                new SessionController().SetTimeout(60 * 24 * 7 * 2);

                return LoginCode.Success;
            }
            else
            {
                return LoginCode.Failed;
            }
        }
        public bool IsSuperAdminLoggedIn()
        {
            return new SessionController().GetSuperAdmin() == true;
        }
        public bool IsAdminLoggedIn()
        {
            return new SessionController().GetAdmin() == true;
        }
        public bool IsDoorManLoggedIn()
        {
            return new SessionController().GetDoorMan() == true;
        }
        public Admin GetCurrentUserAccount()
        {
            Admin admin = null;
            int? id = new SessionController().GetCurrentUserId();
            if (id == null) return null;
            admin = adminDao.FindByID(id.Value);
            return admin;
        }

        //public bool RegisterUser(string name, string surname, string nickname, string email, EncryptedPass pass, string mobile, string note)
        //{
        //    User user = userDao.FindByEmail(email);
        //    if (user != null)
        //    {
        //        return false;
        //    }
        //    user = new User();
        //    user.Name = name;
        //    user.Surname = surname;
        //    user.NickName = nickname;
        //    user.Mobile = mobile;
        //    user.Note = note;
        //    user.Email = email;
        //    user.Password = pass.Encrypted;
        //    user.Role = (int)Role.USER;

        //    bool result = userDao.Insert(user);

        //    return result;
        //}
    }
}