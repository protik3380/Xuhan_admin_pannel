using System;
using System.Linq;
using System.Web.Mvc;
using xtbdEcommerceAdminPanel.Manager;
using xtbdEcommerceAdminPanel.Models.View_Model;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly OrderManager _orderManager;
        public DashBoardController()
        {
            _orderManager = new OrderManager();
        }
     
        public ActionResult Product()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                CountDashboardVM count = new ReportManager().CountDashboard();
                ViewBag.Count = count;
                Session["activeClass"] = "Dashboard";
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult Order()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                //Session["OrderCount"] = _orderManager.CountAllOrder();
                Session["activeClass"] = "Dashboard";
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
    }
}