using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                ViewBag.Customers = Dropdown.Customers()
                    .Where(x => !x.IsDeleted)
                    .OrderBy(x => x.Name).ToList();

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}