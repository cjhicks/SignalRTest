using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRTest.Hubs
{
    public class NotificationHub : Hub
    {
        public void ClientMessage(bool all, string message)
        {
            if (all)
            {
                Clients.All.sendMessage("client message to All: " + message);
            }
            else
            {
                Clients.Others.sendMessage("client message to Others: " + message);
            }
        }
    }
}