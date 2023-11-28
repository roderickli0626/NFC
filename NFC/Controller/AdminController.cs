using NFC.DAO;
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

    }
}