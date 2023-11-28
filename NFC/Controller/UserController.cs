using NFC.DAO;
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
    }
}