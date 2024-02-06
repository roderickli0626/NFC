using Microsoft.VisualBasic.ApplicationServices;
using NFC.DAO;
using NFC.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Twilio.Rest.Preview.Wireless.Sim;

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

        public bool SaveUser(int? userID, string name, string surname, string email, string targa, string city, string phone, string mobile, int? type, string UID, string note, string box)
        {
            User testUser = userDAO.FindByUID(UID);
            if (testUser != null) return false;

            User user = userDAO.FindByID(userID ?? 0);
            if (user == null)
            {
                user = new User();
                user.UserName = name;
                user.Surname = surname;
                user.Email = email;
                user.Targa = targa;
                user.City = city;
                user.Phone = phone;
                user.Mobile = mobile;
                user.TypeOfTag = type;
                user.UID = UID;
                user.Note = note;
                user.BOX = box;
                user.IsEnabled = true;

                return userDAO.Insert(user);
            }
            else
            {
                user.UserName = name;
                user.Surname = surname;
                user.Email = email;
                user.Targa = targa;
                user.City = city;
                user.Phone = phone;
                user.Mobile = mobile;
                user.TypeOfTag = type;
                user.UID = UID;
                user.Note = note;
                user.BOX = box;

                return userDAO.Update(user);
            }
        }

        public bool ImportCSV(List<string[]> rows, Dictionary<int, string> headerPairs)
        {
            bool result = true;
            List<Place> places = new PlaceDAO().FindAll();
            PlaceAccessDAO placeAccessDAO = new PlaceAccessDAO();
            for (int i = 1; i < rows.Count; i++)
            {
                string box = rows[i][headerPairs.FirstOrDefault(x => x.Value == "Box").Key];
                string code = rows[i][headerPairs.FirstOrDefault(x => x.Value == "Code").Key];
                User user = userDAO.FindByBox(box);
                if (user == null)
                {
                    user = new User();
                    user.UserName = rows[i][headerPairs.FirstOrDefault(x => x.Value == "Nome").Key];
                    user.Surname = rows[i][headerPairs.FirstOrDefault(x => x.Value == "Cognome").Key];
                    user.Targa = rows[i][headerPairs.FirstOrDefault(x => x.Value == "Targa").Key];
                    user.BOX = rows[i][headerPairs.FirstOrDefault(x => x.Value == "Box").Key];
                    user.Mobile = rows[i][headerPairs.FirstOrDefault(x => x.Value == "Mobile").Key];
                    user.IsEnabled = true;
                    string eD = rows[i][headerPairs.FirstOrDefault(x => x.Value == "DataScadenza").Key];
                    DateTime? expireDate = null;
                    if (eD != "") expireDate = DateTime.ParseExact(eD, "dd/MM/yyyy HH.mm", CultureInfo.InvariantCulture);
                    int userId = userDAO.Insert1(user);

                    if (string.IsNullOrEmpty(code))
                    {
                        foreach(Place place in places)
                        {
                            PlaceAccess placeAccess = new PlaceAccess();
                            placeAccess.PlaceID = place.Id;
                            placeAccess.ExpireDate = expireDate;
                            placeAccess.UserID = userId;
                            placeAccessDAO.Insert(placeAccess);
                        }
                    }
                    else if (code == "000")
                    {
                        foreach (Place place in places)
                        {
                            PlaceAccess placeAccess = new PlaceAccess();
                            placeAccess.PlaceID = place.Id;
                            placeAccess.UserID = userId;
                            placeAccess.ExpireDate = DateTime.ParseExact("31/12/2199 00.00", "dd/MM/yyyy HH.mm", CultureInfo.InvariantCulture); ;
                            placeAccessDAO.Insert(placeAccess);
                        }
                    }
                    else
                    {
                        user.IsEnabled = false;
                        user.Id = userId;
                        userDAO.Update(user);
                    }
                }
                else
                {
                    user.UserName = rows[i][headerPairs.FirstOrDefault(x => x.Value == "Nome").Key];
                    user.Surname = rows[i][headerPairs.FirstOrDefault(x => x.Value == "Cognome").Key];
                    user.Targa = rows[i][headerPairs.FirstOrDefault(x => x.Value == "Targa").Key];
                    user.BOX = rows[i][headerPairs.FirstOrDefault(x => x.Value == "Box").Key];
                    user.Mobile = rows[i][headerPairs.FirstOrDefault(x => x.Value == "Mobile").Key];
                    string eD = rows[i][headerPairs.FirstOrDefault(x => x.Value == "DataScadenza").Key];
                    DateTime? expireDate = null;
                    if (eD != "") expireDate = DateTime.ParseExact(eD, "dd/MM/yyyy HH.mm", CultureInfo.InvariantCulture);

                    result = result && userDAO.Update(user);

                    if (string.IsNullOrEmpty(code))
                    {
                        foreach (Place place in places)
                        {
                            PlaceAccess placeAccess = placeAccessDAO.FindByUserAndPlace(user.Id, place.Id);
                            if (placeAccess != null)
                            {
                                placeAccess.ExpireDate = expireDate;
                                placeAccessDAO.Update(placeAccess);
                            }
                            else
                            {
                                placeAccess = new PlaceAccess();
                                placeAccess.PlaceID = place.Id;
                                placeAccess.ExpireDate = expireDate;
                                placeAccess.UserID = user.Id;
                                placeAccessDAO.Insert(placeAccess);
                            }
                        }
                    }
                    else if (code == "000")
                    {
                        foreach (Place place in places)
                        {
                            PlaceAccess placeAccess = placeAccessDAO.FindByUserAndPlace(user.Id, place.Id);
                            if (placeAccess != null)
                            {
                                placeAccess.ExpireDate = DateTime.ParseExact("31/12/2199 00.00", "dd/MM/yyyy HH.mm", CultureInfo.InvariantCulture); ;
                                placeAccessDAO.Update(placeAccess);
                            }
                            else
                            {
                                placeAccess = new PlaceAccess();
                                placeAccess.PlaceID = place.Id;
                                placeAccess.ExpireDate = DateTime.ParseExact("31/12/2199 00.00", "dd/MM/yyyy HH.mm", CultureInfo.InvariantCulture); ;
                                placeAccess.UserID = user.Id;
                                placeAccessDAO.Insert(placeAccess);
                            }
                        }
                    }
                    else
                    {
                        user.IsEnabled = false;
                        userDAO.Update(user);
                    }
                }
            }
            return result;
        }
    }
}