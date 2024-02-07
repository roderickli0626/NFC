using NFC.Controller;
using NFC.Util;
using System;
using System.Collections.Generic;
using System.IO;
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
            string testRequest = Request.Url.AbsoluteUri;
            // Add Log file with Data From Reader
            string data = testRequest.Substring(testRequest.IndexOf('?') + 1);
            data = data.Replace("&", ", ");
            string logFilePath = Server.MapPath("~/App_Data/log.txt"); // Path to the log file
            // Write the data to the log file
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine(DateTime.Now.ToString() + " - " + data);
            }


            string CMD = Request.Params["cmd"];
            string UID = Request.Params["uid"];
            string PlaceIP = Request.Params["id"];
            string MD5 = Request.Params["md5"];
            string MAC = Request.Params["mac"];

            int outIn = ParseUtil.TryParseInt(Request.Params["OutIn"]) ?? 1;

            BasicController basicController = new BasicController();
            if (outIn == 1)
            {
                if (CMD == "CO")
                {
                    bool success = basicController.ReadInNFC(UID, PlaceIP);
                }
            }
            else
            {
                if (CMD == "CO")
                {
                    bool success = basicController.ReadOutNFC(UID, PlaceIP);
                }
            }
        }
    }
}