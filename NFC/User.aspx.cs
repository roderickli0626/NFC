﻿using Microsoft.Ajax.Utilities;
using Microsoft.VisualBasic.FileIO;
using NFC.Common;
using NFC.Controller;
using NFC.DAO;
using NFC.Util;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using WhatsAppApi;
using FieldType = Microsoft.VisualBasic.FileIO.FieldType;

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
            string targa = TxtTarga.Text;
            string city = TxtCity.Text;
            string phone = TxtPhone.Text;
            string mobile = TxtMobile.Text;
            int? type = ControlUtil.GetSelectedValue(ComboType1);
            string UID = TxtUID.Text;
            string note = TxtNote.Text;
            string box = TxtBox.Text;

            int? userID = ParseUtil.TryParseInt(HfUserID.Value);

            bool success = new UserController().SaveUser(userID, name, surname, email, targa, city, phone, mobile, type, UID, note, box);
            if (success)
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            else
            {
                ServerValidator.ErrorMessage = "UID già registrato";
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

                // Show an alert to confirm message sent
                string script = "alert('Message Sent!');location.reload();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", script, true);
            }
            else
            {
                List<User> userList = new UserDAO().FindAll();
                foreach (User user in userList)
                {
                    string toPhoneNum = user.Mobile;

                    SendWhatsAppMsg(toPhoneNum, message);
                }

                // Show an alert to confirm message sent
                string script = "alert('Message Sent!');location.reload();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", script, true);
            }

            //if (success)
            //{
            //    Page.Response.Redirect(Page.Request.Url.ToString(), true);
            //}
            //else
            //{
            //    CustomValidator1.IsValid = false;
            //    return;
            //}
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

        protected void BtnImportUser_Click(object sender, EventArgs e)
        {
            if (!FileUploadCSV.HasFile)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select a file to import.');", true);
                return;
            }

            byte[] data = FileUploadCSV.FileBytes;
            List<string[]> rows = ReadCsvRows(data);
            if (rows.Count < 1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('The import file is empty.');", true);
                return;
            }
            else if (rows.Count < 2)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('The import file has no data except headers.');", true);
                return;
            }

            string[] headers = rows[0];
            Dictionary<int, string> headerPairs = new Dictionary<int, string>();
            for (int i = 0; i < headers.Length; i++)
            {
                headerPairs.Add(i, headers[i]);
            }

            bool result = new UserController().ImportCSV(rows, headerPairs);

            if (result)
            {
                string script = "alert('Successfully Imported!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", script, true);
            }
            else
            {
                string script = "alert('Importing Failed!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", script, true);
            }
        }

        public static List<string[]> ReadCsvRows(byte[] content)
        {
            List<string[]> rows = new List<string[]>();
            using (MemoryStream ms = new MemoryStream(content))
            {
                using (TextFieldParser parser = new TextFieldParser(ms))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {
                        //Processing row
                        string[] fields = parser.ReadFields();
                        rows.Add(fields);
                    }
                }
            }
            return rows;
        }
    }
}