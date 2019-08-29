using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using NewMoney.Models;
using System.Windows.Forms;

namespace NewMoney.Controllers
{
    
    public class HomeController : Controller
    {
        public ApplicationDbContext MyContext = new ApplicationDbContext();
        // get: /home/index or /
        public ActionResult Index()
        {
            ApplicationUser currentUser = MyContext.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
            ViewBag.User = currentUser;
            return View();
        }

        // get: /home/dashboard
        public ActionResult Dashboard()
        {
            ApplicationUser currentUser = MyContext.Users.Where(u => u.Email == @User.Identity.Name).FirstOrDefault();
            ViewBag.User = currentUser;
            return View();
        }
// END OF ABOVE ROUTE SET // *********************************************************************************************
        // get: /home/addbits
        public ActionResult AddBits()
        {
            return View();
        }
        // post: /home/buybits
        public ActionResult BuyBits()
        {
            System.Diagnostics.Debug.WriteLine("**********************POST ROUTE HIT SUCCESSFULLY**************************");
            return Redirect("/home/success");
        }
// END OF ABOVE ROUTE SET // *********************************************************************************************
        // get: /home/sendbits
        public ActionResult SendBits()
        {
            return View();
        }
        // post: /home/shipbits
        public ActionResult ShipBits()
        {
            System.Diagnostics.Debug.WriteLine("**********************POST ROUTE HIT SUCCESSFULLY**************************");
            return Redirect("/home/success");
        }
// END OF ABOVE ROUTE SET // *********************************************************************************************
        // get: /home/cashoutbits
        public ActionResult CashOutBits()
        {
            return View();
        }
        // post: /home/cashtobits
        public ActionResult CashToBits()
        {
            System.Diagnostics.Debug.WriteLine("**********************POST ROUTE HIT SUCCESSFULLY**************************");
            return Redirect("/home/success");
        }
// END OF ABOVE ROUTE SET // *********************************************************************************************
        // get: /home/managecircle
        public ActionResult ManageCircle()
        {
            return View();
        }
        // post: /home/addfriend/{FriendId}
        public ActionResult AddFriend()
        {
            System.Diagnostics.Debug.WriteLine("**********************POST ROUTE HIT SUCCESSFULLY**************************");
            return Redirect("/home/success");
        }
        // post: /home/deletefriend/{FriendId}
        public ActionResult DeleteFriend()
        {
            System.Diagnostics.Debug.WriteLine("**********************POST ROUTE HIT SUCCESSFULLY**************************");
            return Redirect("/home/success");
        }
// END OF ABOVE ROUTE SET // *********************************************************************************************
        // get: /home/success
        public ActionResult Success()
        {
            return View();
        }
// END OF ABOVE ROUTE SET // *********************************************************************************************
    }
}