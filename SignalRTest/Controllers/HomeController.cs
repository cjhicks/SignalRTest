using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using SignalRTest.Hubs;

namespace SignalRTest.Controllers
{
    public class HomeController : Controller
    {

        private readonly HubHelper _hubHelper = HubHelper.GetInstance();

        public ActionResult Index()
        {
            _hubHelper.ServerMessageToAll("Somebody hit /Home/Index");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            _hubHelper.ServerMessageToAll("Somebody hit /Home/About");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            _hubHelper.ServerMessageToAll("Somebody hit /Home/Contact");
            return View();
        }

        // this works, just don't really need the functionality right now
        //public JsonResult FakePost(int fakeData)
        //{
        //    _hubHelper.ServerMessageToAll("fake POST hit!");
        //    return Json(true);
        //}

        private void SendMessage(string message)
        {
            GlobalHost.ConnectionManager.GetHubContext<NotificationHub>().Clients.All.sendMessage(message);
        }
    }
}