using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFC
{
    public class SignalRHub : Hub
    {
        public void SendAccessNotification(string message)
        {
            Clients.All.receiveAccessNotification(message);
        }
    }
}