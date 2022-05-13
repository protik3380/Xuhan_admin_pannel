using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using xtbdEcommerceAdminPanel.Manager;
using xtbdEcommerceAdminPanel.Models.RequestDto;
using xtbdEcommerceAdminPanel.Models.View_Model;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class ReportController : Controller
    {
        private readonly ReportManager _reportManager;
        public ReportController()
        {
            _reportManager = new ReportManager();
        }
        public ActionResult SalesByProduct()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                ViewBag.Categories = Dropdown.ActiveCategories();
                ViewBag.Brands = Dropdown.ActiveBrands();
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ShowSalesByProductReport(SalesByProductDto searchCriteria)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                var reportData = _reportManager.GetSaleByProductReportData(searchCriteria);
                ViewBag.Sales = reportData;
                ViewBag.Categories = Dropdown.ActiveCategories();
                ViewBag.Brands = Dropdown.ActiveBrands();
                return View("SalesByProduct");
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
        public ActionResult TotalOrders()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                ViewBag.Categories = Dropdown.ActiveCategories();
                ViewBag.Brands = Dropdown.ActiveBrands();
                ViewBag.MasterDepots = Dropdown.ActiveMasterDepots();
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }


        }

        public ActionResult ShowTotalOrderReport(ReportSearchCriteria searchCriteria)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                var reportData = _reportManager.GetOrderReportData(searchCriteria);
                ViewBag.Sales = reportData;

                ViewBag.Categories = Dropdown.ActiveCategories();
                ViewBag.Brands = Dropdown.ActiveBrands();
                ViewBag.MasterDepots = Dropdown.ActiveMasterDepots();

                return View("TotalOrders");
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult OrdersByStatus()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                ViewBag.Categories = Dropdown.ActiveCategories();
                ViewBag.Brands = Dropdown.ActiveBrands();
                ViewBag.MasterDepots = Dropdown.ActiveMasterDepots();
                ViewBag.OrderStates = Dropdown.OrderStates();
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }


        public ActionResult ShowOrderByStatusReport(OrderByStatusDto searchCriteria)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                var reportData = _reportManager.GetOrderReportByStatus(searchCriteria);
                ViewBag.Sales = reportData;

                ViewBag.Categories = Dropdown.ActiveCategories();
                ViewBag.Brands = Dropdown.ActiveBrands();
                ViewBag.MasterDepots = Dropdown.ActiveMasterDepots();
                ViewBag.OrderStates = Dropdown.OrderStates();

                return View("OrdersByStatus");
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
        public ActionResult OrdersByLocationReport()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Districts = Dropdown.ActiveDistricts();
                ViewBag.Thanas = Dropdown.ActiveThanas();
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }


        public ActionResult ShowOrderByLocationReport(OrderLocationReportDto searchDto)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                var reportData = _reportManager.GetOrderByLocationReport(searchDto);
                ViewBag.Sales = reportData;
                ViewBag.Districts = Dropdown.ActiveDistricts();
                ViewBag.Thanas = Dropdown.ActiveThanas();
                return View("OrdersByLocationReport");
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult Analysis()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                ViewBag.Categories = Dropdown.ActiveCategories();
                ViewBag.Brands = Dropdown.ActiveBrands();
                ViewBag.MasterDepots = Dropdown.ActiveMasterDepots();
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ShowAnalysisReport(ReportSearchCriteria searchCriteria)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                List<OrderReportVM> orders = _reportManager.GetOrderReportData(searchCriteria);
                ReturningCustomerVM returningCustomerData = _reportManager.GetReturningCustomer(searchCriteria);
                if (orders != null)
                {
                    var totalOrders = orders.Count();
                    var totalProductQuantity = orders.Sum(c => Convert.ToDecimal(c.TotalProducts));
                    var totalPrice = orders.Sum(c => c.TotalPrice);
                    var totalRevenue = totalProductQuantity * totalPrice;
                    decimal averageOrderValue = 0;
                    if (totalOrders>0)
                    {
                        averageOrderValue = totalRevenue / totalOrders;
                    }    
                    ViewBag.Revenue = totalRevenue;
                    ViewBag.AverageOrderValue = averageOrderValue;
                }

                decimal returningCustomerRate= 0.0M ;
                if (returningCustomerData.TotalUser>0)
                {
                    returningCustomerRate = Convert.ToDecimal(returningCustomerData.ReturningCustomer * 100) / returningCustomerData.TotalUser;
                }
                ViewBag.ReturningCustomerRate = returningCustomerRate;
                ViewBag.Categories = Dropdown.ActiveCategories();
                ViewBag.Brands = Dropdown.ActiveBrands();
                ViewBag.MasterDepots = Dropdown.ActiveMasterDepots();
                return View("Analysis");
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult OrderRateOverTime()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public ActionResult ShowOrderRateOverTime(SalesOverTimeDto salesOverTime)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (salesOverTime.ToMonth < salesOverTime.FromMonth || salesOverTime.ToYear < salesOverTime.FromYear)
            {
                TempData["Class"] = Utility.Utility.Error;
                TempData["Message"] = "To month/year must be greater than from month/year";
                return View("OrderRateOverTime");
            }

            List<OrderVsMonthOrYearVm> order = null;
            if (salesOverTime.ReportType == 1)
            {
                order = _reportManager.GetYearlyOrderRate(salesOverTime);
            }
            else
            {
                order = _reportManager.GetMonthlyOrderRate(salesOverTime);
            }

            ViewBag.ReportData = order;
            return View("OrderRateOverTime");
        }

    }
}