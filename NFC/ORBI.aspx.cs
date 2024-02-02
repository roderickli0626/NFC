using NFC.Controller;
using NFC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFC
{
    public partial class Orbit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CMD = Request.Params["cmd"];
            string UID = Request.Params["uid"];
            string PlaceIP = Request.Params["id"];
            string MD5 = Request.Params["md5"];
            string MAC = Request.Params["mac"];

            int outIn = ParseUtil.TryParseInt(Request.Params["OutIn"]) ?? 1;

            BasicController basicController = new BasicController();
            if (outIn == 1)
            {
                bool success = basicController.ReadInNFC(UID, PlaceIP);
                bool success1 = basicController.ReadInNFC(MAC, PlaceIP);
            }
            else
            {
                bool success = basicController.ReadOutNFC(UID, PlaceIP);
                bool success1 = basicController.ReadOutNFC(MAC, PlaceIP);
            }
        }
    }
}