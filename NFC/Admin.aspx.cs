using NFC.Common;
using NFC.Controller;
using NFC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFC
{
    public partial class Admin1 : System.Web.UI.Page
    {
        Admin admin;
        LoginController loginSystem = new LoginController();
        protected void Page_Load(object sender, EventArgs e)
        {
            admin = loginSystem.GetCurrentUserAccount();
            if (!loginSystem.IsSuperAdminLoggedIn())
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadTagType();
            }
        }
        private void LoadTagType()
        {
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
            string email = TxtEmail.Text;
            string UID = TxtUID.Text;
            int? type = ControlUtil.GetSelectedValue(ComboType1);
            string password = TxtPassword.Text;
            string repeatPW = TxtPasswordRepeat.Text;
            string note = TxtNote.Text;

            if (password != repeatPW)
            {
                PasswordValidator.IsValid = false;
                return;
            }
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                EmailValidator.IsValid = false;
                return;
            }
            EncryptedPass pass = null;
            if (!string.IsNullOrEmpty(password))
            {
                pass = new EncryptedPass() { Encrypted = new CryptoController().EncryptStringAES(password), UnEncrypted = password };
            }
            int? adminID = ParseUtil.TryParseInt(HfAdminID.Value);

            bool success = new AdminController().SaveAdmin(adminID, name, email, pass, note, UID, type);
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
    }
}