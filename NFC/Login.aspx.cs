using NFC.Common;
using NFC.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFC
{
    public partial class Login : System.Web.UI.Page
    {
        LoginController loginSystem = new LoginController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionController sessionController = new SessionController();
                sessionController.LogoutUser();
            }
        }

        protected void BtnSignIn_Click(object sender, EventArgs e)
        {
            if (!IsValid) { return; }

            string email = TxtEmail.Text;
            string password = TxtPassword.Text;
            EncryptedPass pass = new EncryptedPass() { Encrypted = new CryptoController().EncryptStringAES(password), UnEncrypted = password };

            LoginUser(email, pass);
        }

        private void LoginUser(string email, EncryptedPass pass)
        {
            LoginCode loginStatus = loginSystem.LoginAndSaveSession(email, pass);

            if (loginStatus == LoginCode.Success)
            {
                if (loginSystem.IsDoorManLoggedIn())
                {
                    Response.Redirect("~/AccessLog.aspx");
                }
                else
                {
                    Response.Redirect("~/Dashboard.aspx");
                }
            }
            else
            {
                ServerValidator.IsValid = false;
                return;
            }
        }
    }
}