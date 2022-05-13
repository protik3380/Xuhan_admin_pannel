using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using xtbdEcommerceAdminPanel.Enum;
using xtbdEcommerceAdminPanel.Manager;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Models.RequestDto;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class AssignOrderController : Controller
    {
        // GET: AssignOrder
        private readonly OrderManager _orderManager;
        public AssignOrderController()
        {
            _orderManager = new OrderManager();
        }

        public ActionResult AssignDeliveryMan()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                OrderSearchDto searchDto = new OrderSearchDto
                {
                    OrderStateIds = new List<long>
                    {
                        (long) EnumClass.OrderStateEnum.Pending,
                        (long) EnumClass.OrderStateEnum.Processing,
                        (long) EnumClass.OrderStateEnum.Packaging,
                        (long) EnumClass.OrderStateEnum.Shipped,
                        (long) EnumClass.OrderStateEnum.Delivered,
                    }
                };

                ViewBag.DeliverymanList = Dropdown.ActiveDeliverymanList();
                var orders = _orderManager.GetOrderListByStatusIds(searchDto);
                var assignableOrders = orders.Where(x => x.AssignedOrder == null).ToList();
                ViewBag.Orders = assignableOrders;
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }

        }

        [HttpPost]
        public ActionResult AssignOrder(AssignOrderDto assignOrder)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (assignOrder != null)
            {
                long userId = Convert.ToInt64(Session["UserId"]);
                assignOrder.IsDelivered = false;
                assignOrder.AssignedBy = userId;
                using (var client = new HttpClientDemo())
                {
                    var postTask = client.PostAsJsonAsync<AssignOrderDto>("assignOrder/Create", assignOrder);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Order assigned successfully.";
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        TempData["Class"] = Utility.Utility.Error;
                        TempData["Message"] = "This order is already assigned.";
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeliveryManDetailsById(long deliveryManId)
        {
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("deliveryman/edit/" + deliveryManId);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Deliveryman>();
                    readTask.Wait();
                    var deliveryman = readTask.Result;
                    return Json(new { Status = "Ok", DeliveryMan = deliveryman });
                }
            }

            return Json(new { Status = "Failed" });
        }

        [HttpGet]
        public ActionResult ViewOrderDetails(long id)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("order/getAllOrderById/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Order>();
                        readTask.Wait();
                        var aOrder = readTask.Result;
                        return PartialView(aOrder);
                    }

                    return RedirectToAction("AssignDeliveryMan", "AssignOrder");
                }
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ViewAssignOrder()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                IEnumerable<AssignOrder> assignOrders = null;
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("assignOrder/getAll");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<AssignOrder>>();
                        readTask.Wait();
                        assignOrders = readTask.Result;

                    }
                    else
                    {
                        assignOrders = new List<AssignOrder>();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                ViewBag.DeliveryMen = Dropdown.ActiveDeliverymanList();
                return View(assignOrders);
            }
            catch (Exception ex)
            {
                
                return RedirectToAction("Error", "Home");
            }

        }

        public ActionResult ChangeAssignedDeliveryMan(AssignOrderDto assignOrder)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (assignOrder != null)
            {
                long userId = Convert.ToInt64(Session["UserId"]);
                assignOrder.ModifiedBy = userId;
                using (var client = new HttpClientDemo())
                {
                    var postTask = client.PostAsJsonAsync<AssignOrderDto>("assignOrder/Update", assignOrder);
                    postTask.Wait();
                    var result = postTask.Result;
                    var data1 = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Order assigned to new delivery man successfully.";
                    }
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }

    }
}