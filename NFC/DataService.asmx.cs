using NFC.Controller;
using NFC.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace NFC
{
    /// <summary>
    /// Summary description for DataService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DataService : System.Web.Services.WebService
    {
        LoginController loginSystem = new LoginController();
        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindAdmins(int draw, int start, int length, string searchVal)
        {
            Admin admin = loginSystem.GetCurrentUserAccount();
            if (!loginSystem.IsSuperAdminLoggedIn()) return;

            AdminController adminController = new AdminController();
            SearchResult searchResult = adminController.Search(start, length, searchVal);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void DeleteAdmin(int id)
        {
            //Is Logged in?
            if (!loginSystem.IsSuperAdminLoggedIn()) return;

            AdminController adminController = new AdminController();
            bool success = adminController.DeleteAdmin(id);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindAccessLogs(int draw, int start, int length, string searchVal, int type, string searchFrom, string searchTo)
        {
            Admin admin = loginSystem.GetCurrentUserAccount();
            if (!loginSystem.IsSuperAdminLoggedIn() && (admin == null)) return;

            DateTime? from = null;
            DateTime? to = null;

            if (!string.IsNullOrEmpty(searchFrom))
                from = DateTime.ParseExact(searchFrom, "dd/MM/yyyy HH.mm", CultureInfo.InvariantCulture);

            if (!string.IsNullOrEmpty(searchTo))
                to = DateTime.ParseExact(searchTo, "dd/MM/yyyy HH.mm", CultureInfo.InvariantCulture);

            BasicController basicController = new BasicController();
            SearchResult searchResult = basicController.SearchLog(start, length, searchVal, type, from, to);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindDashboardAccessLogs(int draw, int start, int length, int key)
        {
            Admin admin = loginSystem.GetCurrentUserAccount();
            if (!loginSystem.IsSuperAdminLoggedIn() && (admin == null)) return;

            BasicController basicController = new BasicController();
            SearchResult searchResult = basicController.SearchDashboardLog(start, length, key);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void DeleteAccessLog(int id)
        {
            //Is Logged in?
            Admin admin = loginSystem.GetCurrentUserAccount();
            if (!loginSystem.IsSuperAdminLoggedIn() && (admin == null)) return;

            BasicController basicController = new BasicController();
            bool success = basicController.DeleteAccessLog(id);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindUsers(int draw, int start, int length, string searchVal, int type)
        {
            Admin admin = loginSystem.GetCurrentUserAccount();
            if (!loginSystem.IsSuperAdminLoggedIn() && (admin == null)) return;

            UserController userController = new UserController();
            SearchResult searchResult = userController.Search(start, length, searchVal, type);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void DeleteUser(int id)
        {
            //Is Logged in?
            Admin admin = loginSystem.GetCurrentUserAccount();
            if (!loginSystem.IsSuperAdminLoggedIn() && (admin == null)) return;

            UserController userController = new UserController();
            bool success = userController.DeleteUser(id);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void EnableUser(int id)
        {
            //Is Logged in?
            Admin admin = loginSystem.GetCurrentUserAccount();
            if (!loginSystem.IsSuperAdminLoggedIn() && (admin == null)) return;

            UserController userController = new UserController();
            bool success = userController.EnableUser(id);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindPlaces(int draw, int start, int length, int userID)
        {
            Admin admin = loginSystem.GetCurrentUserAccount();
            if (!loginSystem.IsSuperAdminLoggedIn() && (admin == null)) return;

            BasicController basicController = new BasicController();
            SearchResult searchResult = basicController.SearchPlaces(start, length, userID);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void DeletePlace(int id)
        {
            //Is Logged in?
            Admin admin = loginSystem.GetCurrentUserAccount();
            if (!loginSystem.IsSuperAdminLoggedIn() && (admin == null)) return;

            BasicController basicController = new BasicController();
            bool success = basicController.DeletePlace(id);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AddPlace(int userID, int placeID, string expireDate, string note)
        {
            //Is Logged in?
            Admin admin = loginSystem.GetCurrentUserAccount();
            if (!loginSystem.IsSuperAdminLoggedIn() && (admin == null)) return;

            DateTime? expireDate1 = null;

            if (!string.IsNullOrEmpty(expireDate))
                expireDate1 = DateTime.ParseExact(expireDate, "dd/MM/yyyy HH.mm", CultureInfo.InvariantCulture);

            BasicController basicController = new BasicController();
            bool success = basicController.SavePlace(userID, placeID, expireDate1, note);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetChart1Data()
        {
            HttpResponse Response = Context.Response;
            ProcResult result = new ProcResult();
            Response.ContentType = "application/json; charset=utf-8";

            try
            {
                BasicController basicController = new BasicController();
                result.data = basicController.GetChart1Data();
                result.success = true;
                Response.Write(serializer.Serialize(result));
            }
            catch (Exception ex)
            {
                result.success = false;
                Response.Write(serializer.Serialize(result));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetChart3Data()
        {
            HttpResponse Response = Context.Response;
            ProcResult result = new ProcResult();
            Response.ContentType = "application/json; charset=utf-8";

            try
            {
                BasicController basicController = new BasicController();
                result.data = basicController.GetChart3Data();
                result.success = true;
                Response.Write(serializer.Serialize(result));
            }
            catch (Exception ex)
            {
                result.success = false;
                Response.Write(serializer.Serialize(result));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetChart2Data()
        {
            HttpResponse Response = Context.Response;
            ProcResult result = new ProcResult();
            Response.ContentType = "application/json; charset=utf-8";

            try
            {
                BasicController basicController = new BasicController();
                result.data = basicController.GetChart2Data();
                result.success = true;
                Response.Write(serializer.Serialize(result));
            }
            catch (Exception ex)
            {
                result.success = false;
                Response.Write(serializer.Serialize(result));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void ReadInNFC(string UID, string PlaceIP)
        {
            BasicController basicController = new BasicController();
            bool success = basicController.ReadInNFC(UID, PlaceIP);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void ReadOutNFC(string UID, string PlaceIP)
        {
            BasicController basicController = new BasicController();
            bool success = basicController.ReadOutNFC(UID, PlaceIP);

            ResponseProc(success, "");
        }

        protected void ResponseJson(Object result)
        {
            HttpResponse Response = Context.Response;
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(serializer.Serialize(result));
        }
        protected void ResponseJson(Object result, bool success)
        {
            HttpResponse Response = Context.Response;
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(serializer.Serialize(result));
            if (success)
            {
                Response.StatusCode = 200;
            }
            Response.Flush();
        }

        protected void ResponseProc(bool success, object data, string message = "")
        {
            ProcResult result = new ProcResult();
            result.success = success;
            result.data = data;
            result.message = message;
            ResponseJson(result, success);
        }
    }
}
