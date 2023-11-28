using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFC.Model
{
    public class UserCheck
    {
        User user;
        public UserCheck() { }
        public UserCheck(User user) 
        { 
            this.user = user;
            if (user == null) { return; }
            Id = user.Id;
            Name = user.UserName;
            NFC = user.NFC;
            Role = user.Role;
            ExpireDate = user.ExpireDate?.ToString("dd/MM/yyyy HH.mm");
            Note = user.Note;
        }
        public int Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public string NFC
        {
            get; set;
        }
        public int? Role
        {
            get; set;
        }
        public string ExpireDate
        {
            get; set;
        }
        public string Note
        {
            get; set;
        }
    }
}