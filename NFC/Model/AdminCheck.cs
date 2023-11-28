using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFC.Model
{
    public class AdminCheck
    {
        Admin admin;
        public AdminCheck() { }
        public AdminCheck(Admin admin)
        {
            this.admin = admin;
            if (admin == null) return;
            Id = admin.Id;
            Name = admin.Name;
            Email = admin.Email;
            Note = admin.Note;
        }
        public int Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public string Email
        {
            get; set;
        }
        public string Note
        {
            get; set;
        }
    }
}