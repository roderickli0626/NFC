using NFC.Common;
using NFC.Controller;
using NFC.DAO;
using NFC.Util;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using WhatsAppApi;

namespace NFC
{
    public partial class User1 : System.Web.UI.Page
    {
        Admin admin;
        LoginController loginSystem = new LoginController();
        protected void Page_Load(object sender, EventArgs e)
        {
            admin = loginSystem.GetCurrentUserAccount();
            if (!loginSystem.IsSuperAdminLoggedIn() && (admin == null))
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadTagType();
                LoadPlace();
            }
        }
        private void LoadPlace()
        {
            List<Place> places = new PlaceDAO().FindAll();
            ControlUtil.DataBind(ComboPlace, places, "Id", "PlaceTitle");
        }
        private void LoadTagType()
        {
            ComboType.Items.Clear();
            ComboType.Items.Add(new ListItem("TUTTI", "0"));
            ComboType.Items.Add(new ListItem("TELECOMANDO", ((int)TagType.BUTTON).ToString()));
            ComboType.Items.Add(new ListItem("RFID", ((int)TagType.RFID).ToString()));
            ComboType.Items.Add(new ListItem("TAG", ((int)TagType.TAG).ToString()));
            ComboType.Items.Add(new ListItem("NFC", ((int)TagType.NFC).ToString()));

            ComboType1.Items.Clear();
            ComboType1.Items.Add(new ListItem("", ""));
            ComboType1.Items.Add(new ListItem("TELECOMANDO", ((int)TagType.BUTTON).ToString()));
            ComboType1.Items.Add(new ListItem("RFID", ((int)TagType.RFID).ToString()));
            ComboType1.Items.Add(new ListItem("TAG", ((int)TagType.TAG).ToString()));
            ComboType1.Items.Add(new ListItem("NFC", ((int)TagType.NFC).ToString()));
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;
            string name = TxtName.Text;
            string surname = TxtSurname.Text;
            string email = TxtEmail.Text;
            string address = TxtAddress.Text;
            string city = TxtCity.Text;
            string phone = TxtPhone.Text;
            string mobile = TxtMobile.Text;
            int? type = ControlUtil.GetSelectedValue(ComboType1);
            string UID = TxtUID.Text;
            string note = TxtNote.Text;

            int? userID = ParseUtil.TryParseInt(HfUserID.Value);

            bool success = new UserController().SaveUser(userID, name, surname, email, address, city, phone, mobile, type, UID, note);
            if (success)
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            else
            {
                ServerValidator.IsValid = false;
                return;
            }
        }

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            string message = TxtMsg.Text;
            if (string.IsNullOrEmpty(message)) 
            {
                CustomValidator0.IsValid = false;
                return; 
            }
            int userID = ParseUtil.TryParseInt(HfUserID.Value) ?? 0;
            bool success = true;

            if (userID != 0)
            {
                User user = new UserDAO().FindByID(userID);
                string toPhoneNum = user.Mobile;

                SendWhatsAppMsg(toPhoneNum, message);
            }
            else
            {
                List<User> userList = new UserDAO().FindAll();
                foreach (User user in userList)
                {
                    string toPhoneNum = user.Mobile;

                    SendWhatsAppMsg(toPhoneNum, message);
                }
            }

            if (success)
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            else
            {
                CustomValidator1.IsValid = false;
                return;
            }
        }

        private async Task SendWhatsAppMsg(string toPhoneNum, string message)
        {
            var url = "https://api.ultramsg.com/instance71748/messages/chat";
            var client = new RestClient(url);

            var request = new RestRequest(url, Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("token", "cq5s6q6y8hp7478g");
            request.AddParameter("to", toPhoneNum);
            request.AddParameter("body", message);

            RestResponse response = await client.ExecuteAsync(request);
            var output = response.Content;
            Console.WriteLine(output);
        }
    }
}