using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task AddToGroup(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.Remove(Context.ConnectionId, groupName);
        }

        public void ClientMessageToGroup(string groupName, string message)
        {
            Clients.Group(groupName).sendMessage("Group " + groupName + ": " + message);
        }
    }
}