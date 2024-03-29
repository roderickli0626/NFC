﻿using NFC.Common;
using NFC.DAO;
using NFC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Reflection.Emit;
using NFC.Util;

namespace NFC.Controller
{
    public class BasicController
    {
        private AccessLogDAO accessLogDAO;
        private PlaceDAO placeDAO;
        private PlaceAccessDAO placeAccessDAO;
        private UserDAO userDAO;
        public BasicController() 
        { 
            accessLogDAO = new AccessLogDAO();
            placeDAO = new PlaceDAO();
            placeAccessDAO = new PlaceAccessDAO();
            userDAO = new UserDAO();
        }

        public SearchResult SearchLog(int start, int length, string searchVal, int type, DateTime? from, DateTime? to)
        {
            SearchResult result = new SearchResult();
            IEnumerable<AccessLog> logList = accessLogDAO.FindAll().OrderByDescending(m => m.AccessDate);
            if (!string.IsNullOrEmpty(searchVal))
            {
                if (searchVal.ToLower() == "In".ToLower() || searchVal.ToLower() == "Out".ToLower())
                {
                    bool logINs = (searchVal.ToLower() == "In".ToLower());
                    logList = logList.Where(m => (m.IsIn ?? false) == logINs);
                }
                else
                {
                    logList = logList.Where(m => (m.User?.UserName.ToLower().Contains(searchVal.ToLower()) ?? false) || (m.User?.UID.Contains(searchVal) ?? false) || (m.Place?.PlaceTitle.ToLower().Contains(searchVal.ToLower()) ?? false) || m.Note.ToLower().Contains(searchVal.ToLower()));
                }
            }
            if (type != 0) logList = logList.Where(l => l.User?.TypeOfTag == type);

            if (from != null)
                logList = logList.Where(u => u.AccessDate >= from.Value);

            if (to != null)
                logList = logList.Where(u => u.AccessDate <= to.Value);

            result.TotalCount = logList.Count();
            logList = logList.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (AccessLog log in logList)
            {
                AccessLogCheck accessLogCheck = new AccessLogCheck(log);
                checks.Add(accessLogCheck);
            }
            result.ResultList = checks;

            return result;
        }

        public SearchResult SearchDashboardLog(int start, int length, int key)
        {
            SearchResult result = new SearchResult();
            IEnumerable<AccessLog> logList = accessLogDAO.FindAll().OrderByDescending(m => m.AccessDate);
            if (key == 1) logList = logList.Where(l => l.AccessDate > DateTime.Now.AddHours(-1) && l.User != null && l.Place != null);
            else if (key == 2) logList  = logList.Where(l => l.AccessDate > DateUtil.startOfDay(DateTime.Now) && l.User != null && l.Place != null);
            else if (key == 3) logList  = logList.Where(l => l.AccessDate > DateUtil.startOfWeek(DateTime.Now) && l.User != null && l.Place != null);
            else if (key == 4) logList  = logList.Where(l => l.AccessDate > DateUtil.startOfMonth(DateTime.Now) && l.User != null && l.Place != null);

            result.TotalCount = logList.Count();
            logList = logList.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (AccessLog log in logList)
            {
                AccessLogCheck accessLogCheck = new AccessLogCheck(log);
                checks.Add(accessLogCheck);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeleteAccessLog(int id)
        {
            AccessLog log = accessLogDAO.FindByID(id);
            if (log == null) return false;

            return accessLogDAO.Delete(id);
        }

        public SearchResult SearchPlaces(int start, int length, int userID)
        {
            SearchResult result = new SearchResult();
            IEnumerable<PlaceAccess> placeList = placeAccessDAO.FindAll().OrderByDescending(m => m.PlaceID);
            placeList = placeList.Where(place => place.UserID == userID);

            result.TotalCount = placeList.Count();
            placeList = placeList.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (PlaceAccess place in placeList)
            {
                PlaceAccessCheck placeCheck = new PlaceAccessCheck(place);
                checks.Add(placeCheck);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeletePlace(int id)
        {
            PlaceAccess place = placeAccessDAO.FindByID(id);
            if (place == null) return false;

            return placeAccessDAO.Delete(id);
        }

        public bool SavePlace(int userID, int placeID, DateTime? expireDate, string note) 
        {
            PlaceAccess place = placeAccessDAO.FindByUserID(userID).Where(p => p.PlaceID == placeID).FirstOrDefault();
            if (place == null)
            {
                place = new PlaceAccess();
                place.PlaceID = placeID;
                place.Note = note;
                place.UserID = userID;
                place.ExpireDate = expireDate;
                return placeAccessDAO.Insert(place);
            }
            else
            {
                place.ExpireDate = expireDate;
                place.Note = note;
                return placeAccessDAO.Update(place);
            }
        }

        public List<List<int>> GetChart1Data()
        {
            List<List<int>> result = new List<List<int>>();

            List<AccessLog> logList = accessLogDAO.FindAll().Where(l => l.AccessDate?.DayOfWeek != DayOfWeek.Saturday && l.AccessDate?.DayOfWeek != DayOfWeek.Sunday).ToList();
            List<AccessLog> buttonLogList = logList.Where(l => l.User?.TypeOfTag == (int)TagType.BUTTON).ToList();
            List<AccessLog> rfidLogList = logList.Where(l => l.User?.TypeOfTag == (int)TagType.RFID).ToList();
            List<AccessLog> tagLogList = logList.Where(l => l.User?.TypeOfTag == (int)TagType.TAG).ToList();
            List<AccessLog> nfcLogList = logList.Where(l => l.User?.TypeOfTag == (int)TagType.NFC).ToList();

            List<int> subResult1 = new List<int>();
            subResult1.Add(buttonLogList.Where(l => l.AccessDate?.Hour >= 6 && l.AccessDate?.Hour < 12).Count());
            subResult1.Add(rfidLogList.Where(l => l.AccessDate?.Hour >= 6 && l.AccessDate?.Hour < 12).Count());
            subResult1.Add(tagLogList.Where(l => l.AccessDate?.Hour >= 6 && l.AccessDate?.Hour < 12).Count());
            subResult1.Add(nfcLogList.Where(l => l.AccessDate?.Hour >= 6 && l.AccessDate?.Hour < 12).Count());

            List<int> subResult2 = new List<int>();
            subResult2.Add(buttonLogList.Where(l => l.AccessDate?.Hour >= 12 && l.AccessDate?.Hour < 18).Count());
            subResult2.Add(rfidLogList.Where(l => l.AccessDate?.Hour >= 12 && l.AccessDate?.Hour < 18).Count());
            subResult2.Add(tagLogList.Where(l => l.AccessDate?.Hour >= 12 && l.AccessDate?.Hour < 18).Count());
            subResult2.Add(nfcLogList.Where(l => l.AccessDate?.Hour >= 12 && l.AccessDate?.Hour < 18).Count());

            List<int> subResult3 = new List<int>();
            subResult3.Add(buttonLogList.Where(l => l.AccessDate?.Hour >= 18 || l.AccessDate?.Hour < 6).Count());
            subResult3.Add(rfidLogList.Where(l => l.AccessDate?.Hour >= 18 || l.AccessDate?.Hour < 6).Count());
            subResult3.Add(tagLogList.Where(l => l.AccessDate?.Hour >= 18 || l.AccessDate?.Hour < 6).Count());
            subResult3.Add(nfcLogList.Where(l => l.AccessDate?.Hour >= 18 || l.AccessDate?.Hour < 6).Count());

            result.Add(subResult1);
            result.Add(subResult2);
            result.Add(subResult3);

            return result;
        }

        public List<int> GetChart3Data()
        {
            List<int> result = new List<int>();
            List<AccessLog> logList = accessLogDAO.FindAll().Where(l => l.AccessDate?.DayOfWeek == DayOfWeek.Saturday || l.AccessDate?.DayOfWeek == DayOfWeek.Sunday).ToList();
            List<AccessLog> buttonLogList = logList.Where(l => l.User?.TypeOfTag == (int)TagType.BUTTON).ToList();
            List<AccessLog> rfidLogList = logList.Where(l => l.User?.TypeOfTag == (int)TagType.RFID).ToList();
            List<AccessLog> tagLogList = logList.Where(l => l.User?.TypeOfTag == (int)TagType.TAG).ToList();
            List<AccessLog> nfcLogList = logList.Where(l => l.User?.TypeOfTag == (int)TagType.NFC).ToList();

            result.Add(buttonLogList.Count());
            result.Add(rfidLogList.Count());
            result.Add(tagLogList.Count());
            result.Add(nfcLogList.Count());

            return result;
        }

        public List<List<int>> GetChart2Data()
        {
            List<List<int>> result = new List<List<int>>();
            //List<AccessLog> logList = accessLogDAO.FindAll().Where(l => l.AccessDate?.DayOfWeek == DayOfWeek.Saturday || l.AccessDate?.DayOfWeek == DayOfWeek.Sunday).ToList();
            List<AccessLog> logList = accessLogDAO.FindAll();
            List<AccessLog> buttonLogList = logList.Where(l => l.User?.TypeOfTag == (int)TagType.BUTTON).ToList();
            List<AccessLog> rfidLogList = logList.Where(l => l.User?.TypeOfTag == (int)TagType.RFID).ToList();
            List<AccessLog> tagLogList = logList.Where(l => l.User?.TypeOfTag == (int)TagType.TAG).ToList();
            List<AccessLog> nfcLogList = logList.Where(l => l.User?.TypeOfTag == (int)TagType.NFC).ToList();

            List<int> buttonResult = new List<int>();
            List<int> rfidResult = new List<int>();
            List<int> tagResult = new List<int>();
            List<int> nfcResult = new List<int>();

            for (int i = 0; i < 12; i++)
            {
                DateTime compareDate = DateTime.Now.AddMonths(-i);
                buttonResult.Insert(0, buttonLogList.Where(b => b.AccessDate?.Year == compareDate.Year && b.AccessDate?.Month == compareDate.Month).Count());
                rfidResult.Insert(0, rfidLogList.Where(b => b.AccessDate?.Year == compareDate.Year && b.AccessDate?.Month == compareDate.Month).Count());
                tagResult.Insert(0, tagLogList.Where(b => b.AccessDate?.Year == compareDate.Year && b.AccessDate?.Month == compareDate.Month).Count());
                nfcResult.Insert(0, nfcLogList.Where(b => b.AccessDate?.Year == compareDate.Year && b.AccessDate?.Month == compareDate.Month).Count());
            }

            result.Add(buttonResult);
            result.Add(rfidResult);
            result.Add(nfcResult);
            result.Add(tagResult);

            return result;
        }

        public bool ReadInNFC(string UIDCode, string PlaceIP)
        {
            

            User user = userDAO.FindByUID(UIDCode);
            Place place = placeDAO.FindAll().Where(p => p.IPAddress == PlaceIP).FirstOrDefault();
            // Admin Access
            Admin admin = new AdminDAO().FindByUID(UIDCode);
            if (admin != null)
            {
                //Send Notification
                var hubContext1 = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                hubContext1.Clients.All.receiveAccessNotification("Accesso in corso");

                AccessLog log = new AccessLog();
                log.AccessDate = DateTime.Now;
                log.AdminID = admin.Id;
                log.IsIn = true;
                log.IsOut = false;
                log.Note = "<i style=\"color:white\">Accesso STAFF</i>";
                if (place != null)
                {
                    log.PlaceID = place.Id;
                    SendCommandToRelay(place);
                }
                return accessLogDAO.Insert(log);
            }

            bool result = false;
            if (user == null || (user.IsEnabled ?? false) == false || place == null || (place.GlobalOpenSetting ?? false) == false)
            {
                //Send Notification
                var hubContext1 = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                hubContext1.Clients.All.receiveAccessErrorNotification("Rilevato accesso non autorizzato");

                AccessLog log = new AccessLog();
                log.AccessDate = DateTime.Now;
                log.AccessDetail = "";
                log.Note = "";
                log.IsIn = true;
                log.IsOut = false;
                if (user == null || place == null)
                {
                    if (user != null) log.UserID = user.Id;
                    if (place != null) log.PlaceID = place.Id;
                    log.Note = "Accesso NON autorizzato";
                }
                else
                {
                    log.UserID = user.Id;
                    log.PlaceID = place.Id;
                    log.Note = "<i style=\"color:red\">Accesso Bloccato</i>";
                }
                accessLogDAO.Insert(log);

                return result; 
            }
            else
            {
                PlaceAccess access = placeAccessDAO.FindAll().Where(p => p.UserID == user.Id && p.PlaceID == place.Id).FirstOrDefault();
                if (access == null)
                {
                    //Send Notification
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                    hubContext.Clients.All.receiveAccessErrorNotification("Rilevato accesso non autorizzato");

                    AccessLog failedlog = new AccessLog();
                    failedlog.AccessDate = DateTime.Now;
                    failedlog.UserID = user.Id;
                    failedlog.AccessDetail = "";
                    failedlog.Note = "<i style=\"color:red\">Accesso Bloccato</i>";
                    failedlog.IsIn = true;
                    failedlog.IsOut = false;
                    failedlog.PlaceID = place.Id;
                    accessLogDAO.Insert(failedlog);

                    return result;
                }

                string note = "<i style=\"color:green\">Accesso Abilitato</i>";
                if (access.ExpireDate < DateTime.Now)
                {
                    note = "<i style=\"color:yellow\">ACCESSO SCADUTO</i>";                        
                }
                AccessLog log = new AccessLog();
                log.AccessDate = DateTime.Now;
                log.UserID = user.Id;
                log.AccessDetail = "";
                log.Note = note;
                log.IsIn = true;
                log.IsOut = false;
                log.PlaceID = place.Id;
                result = accessLogDAO.Insert(log);

                if (access.ExpireDate < DateTime.Now)
                {
                    //Send Notification
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                    hubContext.Clients.All.receiveAccessErrorNotification("Tentativo di Accesso con TAG Scaduto");

                    return result;
                }

                SendCommandToRelay(place);
                //Send Notification
                var hubContext1 = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                hubContext1.Clients.All.receiveAccessNotification("Accesso in corso");

                return result;
            }
        }

        public bool ReadOutNFC(string UIDCode, string PlaceIP)
        {
            User user = userDAO.FindByUID(UIDCode);
            Place place = placeDAO.FindAll().Where(p => p.IPAddress == PlaceIP).FirstOrDefault();
            // Admin Access
            Admin admin = new AdminDAO().FindByUID(UIDCode);
            if (admin != null)
            {
                //Send Notification
                var hubContext1 = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                hubContext1.Clients.All.receiveAccessNotification("Accesso in corso");

                AccessLog log = new AccessLog();
                log.AccessDate = DateTime.Now;
                log.AdminID = admin.Id;
                log.IsIn = false;
                log.IsOut = true;
                log.Note = "<i style=\"color:white\">Accesso STAFF</i>";
                if (place != null)
                {
                    log.PlaceID = place.Id;
                    // SendCommandToRelay(place);
                }
                return accessLogDAO.Insert(log);
            }

            bool result = false;
            if (user == null || (user.IsEnabled ?? false) == false || place == null || (place.GlobalOpenSetting ?? false) == false)
            {
                //Send Notification
                var hubContext1 = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                hubContext1.Clients.All.receiveAccessErrorNotification("Rilevato Accesso Non Autorizzato");

                AccessLog log = new AccessLog();
                log.AccessDate = DateTime.Now;
                log.AccessDetail = "";
                log.Note = "";
                log.IsIn = false;
                log.IsOut = true;
                if (user == null || place == null)
                {
                    if (user != null) log.UserID = user.Id;
                    if (place != null) log.PlaceID = place.Id;
                    log.Note = "Accesso NON autorizzato";
                }
                else
                {
                    log.UserID = user.Id;
                    log.PlaceID = place.Id;
                    log.Note = "<i style=\"color:red\">Accesso Bloccato</i>";
                }
                accessLogDAO.Insert(log);

                return result;
            }
            else
            {
                PlaceAccess access = placeAccessDAO.FindAll().Where(p => p.UserID == user.Id && p.PlaceID == place.Id).FirstOrDefault();
                if (access == null)
                {
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                    hubContext.Clients.All.receiveAccessErrorNotification("Rilevato Accesso Non Autorizzato");

                    AccessLog failedlog = new AccessLog();
                    failedlog.AccessDate = DateTime.Now;
                    failedlog.UserID = user.Id;
                    failedlog.AccessDetail = "";
                    failedlog.Note = "<i style=\"color:red\">Accesso Blocccato</i>";
                    failedlog.IsIn = false;
                    failedlog.IsOut = true;
                    failedlog.PlaceID = place.Id;
                    accessLogDAO.Insert(failedlog);

                    return result;
                }

                string note = "<i style=\"color:green\">Accesso Abilitato</i>";
                if (access.ExpireDate < DateTime.Now)
                {
                    note = "<i style=\"color:yellow\">ACCESSO SCADUTO</i>";
                }
                AccessLog log = new AccessLog();
                log.AccessDate = DateTime.Now;
                log.UserID = user.Id;
                log.AccessDetail = "";
                log.Note = note;
                log.IsOut = true;
                log.IsIn = false;
                log.PlaceID = place.Id;
                result = accessLogDAO.Insert(log);

                if (access.ExpireDate < DateTime.Now)
                {
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                    hubContext.Clients.All.receiveAccessErrorNotification("Tentativo di Accesso con TAG Scaduto");

                    return result;
                }

                // SendCommandToRelay(place);
                //Send Notification
                var hubContext1 = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
                hubContext1.Clients.All.receiveAccessNotification("Accesso in corso");

                return result;
            }
        }

        private async void SendCommandToRelay(Place place)
        {
            try
            {
                string relayIpAddress = place.IPAddress;
                //if (relayIpAddress == null) { return false; }

                using (var client = new HttpClient())
                {
                    var content = new StringContent("open " + place.PlaceTitle, Encoding.UTF8, "text/plain");
                    // Set the credentials
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("ftp:ftp")));
                    var response = await client.GetAsync("http://79.32.32.96:7701/protect/toggle.cgi?toggle=A");

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions or errors that may occur during the command sending process
                Console.WriteLine(ex.Message);
            }

            //return false;
        }

        public bool SetGlobalSetting(bool IsOpen)
        {
            List<Place> placeList = placeDAO.FindAll();
            foreach (Place place in placeList)
            {
                place.GlobalOpenSetting = IsOpen;
                placeDAO.Update(place);
            }
            return true;
        }

        public bool GetGlobalSetting()
        {
            bool result = false;
            Place place = placeDAO.FindAll().FirstOrDefault();
            if (place == null) return result;
            result = place.GlobalOpenSetting ?? false;

            return result;
        }

        public bool SaveBasicPlace(int? placeID, string title, string ipAddress, string note)
        {
            if (placeID != null) 
            {
                Place place = placeDAO.FindByID(placeID ?? 0);
                if (place == null) return false;
                place.PlaceTitle = title;
                place.IPAddress = ipAddress;
                place.Note = note;
                return placeDAO.Update(place);
            }
            else
            {
                Place place = new Place();
                place.PlaceTitle = title;
                place.IPAddress = ipAddress;
                place.Note = note;
                return placeDAO.Insert(place);
            }
        }

        public SearchResult SearchBasicPlaces(int start, int length, string searchVal)
        {
            SearchResult result = new SearchResult();
            IEnumerable<Place> placeList = placeDAO.FindAll();
            placeList = placeList.Where(place => place.PlaceTitle.ToLower().Contains(searchVal.ToLower()));

            result.TotalCount = placeList.Count();
            placeList = placeList.Skip(start).Take(length);

            List<object> checks = new List<object>();
            foreach (Place place in placeList)
            {
                PlaceCheck placeCheck = new PlaceCheck(place);
                checks.Add(placeCheck);
            }
            result.ResultList = checks;

            return result;
        }

        public bool DeleteBasicPlace(int id)
        {
            Place place = placeDAO.FindByID(id);
            if (place == null) return false;

            return placeDAO.Delete(id);
        }
    }
}