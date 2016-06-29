using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRTest.Hubs
{
    public class HubHelper
    {

        private static HubHelper _instance;

        private Timer timer;
        private HubHelper()
        {
            timer = new Timer();
            timer.Interval = 60000;
            timer.Elapsed += TimerOnElapsed;
            timer.Start();
        }

        private int _timeInMinutes = 0;
        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            ServerMessageToAll("Time in app: " + ++_timeInMinutes + " min");
        }

        public static HubHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new HubHelper();
            }
            return _instance;
        }

        public void ServerMessageToAll(string message)
        {
            GlobalHost.ConnectionManager.GetHubContext<NotificationHub>().Clients.All.sendMessage(message);
        }
    }
}