using Microsoft.IdentityModel.Tokens;
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

namespace NFC
{
    public partial class SendMsg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<User> userList = new UserDAO().FindAll();
            foreach (User user in userList)
            {
                PlaceAccess placeAccess = new PlaceAccessDAO().FindByUserID(user.Id).FirstOrDefault();
                DateTime? expireDate = placeAccess?.ExpireDate ?? null;
                if (expireDate != null)
                {
                    if (DateUtil.startOfDay(expireDate.Value) == DateUtil.startOfDay(DateTime.Now.AddDays(15)))
                    {
                        SendWhatsAppMsg(user.Mobile, "Access Will be Expired After 15 days.");
                    }

                    if (DateUtil.startOfDay(expireDate.Value) == DateUtil.startOfDay(DateTime.Now.AddDays(7)))
                    {
                        SendWhatsAppMsg(user.Mobile, "Access Will be Expired After 7 days.");
                    }
                }
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