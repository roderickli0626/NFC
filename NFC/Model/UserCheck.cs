﻿using System;
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
            Surname = user.Surname;
            Mobile = user.Mobile;
            Phone = user.Phone;
            Targa = user.Targa;
            TagType = user.TypeOfTag ?? 0;
            City = user.City;
            Email = user.Email;
            UID = user.UID;
            Note = user.Note;
            BOX = user.BOX;
            IsEnabled = user.IsEnabled ?? false;
        }
        public int Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public string Surname
        {
            get; set;
        }
        public string Phone
        {
            get; set;
        }
        public string Mobile
        {
            get; set;
        }
        public string Targa
        {
            get; set;
        }
        public string Email
        {
            get; set;
        }
        public string City
        {
            get; set;
        }
        public int TagType
        {
            get; set;
        }
        public string UID
        {
            get; set;
        }
        public string Note
        {
            get; set;
        }
        public string BOX
        {
            get; set;
        }
        public bool IsEnabled
        {
            get; set;
        }
    }
}