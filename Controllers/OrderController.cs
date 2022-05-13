using System;
using System.Net.Http;
using System.Web.Mvc;
using xtbdEcommerceAdminPanel.Enum;
using xtbdEcommerceAdminPanel.Manager;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Models.RequestDto;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderManager _orderManager;
        public OrderController()
        {
            _orderManager = new OrderManager();
        }
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GenerateInvoice(long id)
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
                        ViewBag.Order = aOrder;
                        return View();
                    }

                    return RedirectToAction("PendingOrders", "Order");
                }
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
        

        public ActionResult PendingOrders()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Orders = _orderManager.GetOrderListByStatus((int)EnumClass.OrderStateEnum.Pending);
                Session["activeClass"] = "PendingOrders";
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }   
        }

        
        public ActionResult ProcessingOrders()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Orders = _orderManager.GetOrderListByStatus((int)EnumClass.OrderStateEnum.Processing);
                Session["activeClass"] = "ProcessingOrders";
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }



        [HttpGet]
        public ActionResult GetOrderDetails(long id)
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
                        ViewBag.RejectStatus = Dropdown.RejectStatusList();
                        return PartialView(aOrder);
                    }

                    return RedirectToAction("PendingOrders", "Order");
                }         
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }           
        }

        public ActionResult CancelOrder(long orderId, long userId, long rejectId,long orderStateId)
        {
            try
            {
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                using (var client = new HttpClientDemo())
                {
                    string actionName = String.Empty;

                    CancelOrderDto data = new CancelOrderDto();
                    data.UserId = userId;
                    data.OrderId = orderId;
                    data.OrderRejectId = rejectId;
                    data.OrderStateId = (long)EnumClass.OrderStateEnum.Cancelled;


                    if (orderStateId == (long)EnumClass.OrderStateEnum.Pending)
                    {                        
                        actionName = "PendingOrders";                       
                    }
                    else if (orderStateId == (long)EnumClass.OrderStateEnum.Processing)
                    {                      
                        actionName = "ProcessingOrders";                    
                    }
                    else if (orderStateId == (long)EnumClass.OrderStateEnum.Packaging)
                    {                       
                        actionName = "PackagingOrders";                       
                    }
                    else if (orderStateId == (long)EnumClass.OrderStateEnum.Shipped)
                    {                     
                        actionName = "ShippiedOrders";                      
                    }
                    else if (orderStateId == (long)EnumClass.OrderStateEnum.Delivered)
                    {
                        actionName = "DeliveredOrders";
                    }

                    var postTask = client.PostAsJsonAsync("order/cancelOrder", data);
                    postTask.Wait();
                    var result = postTask.Result;
                    var data1 = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Order Cancelled";
                        //Session["OrderCount"] = _orderManager.CountAllOrder();
                    }
                    return RedirectToAction(actionName, "Order");
                }
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ChangeOrderState(long orderId,long userId,long orderStateId)
        {

            try
            {
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                using (var client = new HttpClientDemo())
                {
                    string actionName = String.Empty;
                    string message = String.Empty;

                    OrderStatusChangeDto data = new OrderStatusChangeDto();
                    data.UserId = userId;
                    data.OrderId = orderId;


                    if (orderStateId== (long)EnumClass.OrderStateEnum.Pending)
                    {
                        data.OrderStateId = (long)EnumClass.OrderStateEnum.Processing;
                        actionName = "PendingOrders";
                        message = "Order status change to Processing";
                    }
                    else if (orderStateId == (long)EnumClass.OrderStateEnum.Processing)
                    {
                        data.OrderStateId = (long)EnumClass.OrderStateEnum.Packaging;
                        actionName = "ProcessingOrders";
                        message = "Order status change to Packaging";
                    }
                    else if (orderStateId == (long)EnumClass.OrderStateEnum.Packaging)
                    {
                        data.OrderStateId = (long)EnumClass.OrderStateEnum.Shipped;
                        actionName = "PackagingOrders";
                        message = "Order status change to shipped";
                    }
                    else if (orderStateId == (long)EnumClass.OrderStateEnum.Shipped)
                    {
                        data.OrderStateId = (long)EnumClass.OrderStateEnum.Delivered;
                        actionName = "ShippiedOrders";
                        message = "Order status change to delivered";
                    }
                    else if (orderStateId == (long)EnumClass.OrderStateEnum.Delivered)
                    {
                        data.OrderStateId = (long)EnumClass.OrderStateEnum.Received;
                        actionName = "DeliveredOrders";
                        message = "Order status change to received";
                    }

                    var postTask = client.PostAsJsonAsync("updateOrderState", data);
                    postTask.Wait();
                    var result = postTask.Result;
                    var data1 = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = message;
                        //Session["OrderCount"] = _orderManager.CountAllOrder();
                    }
                    return RedirectToAction(actionName, "Order");
                }
               
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }


        public ActionResult PackagingOrders()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Orders = _orderManager.GetOrderListByStatus((int)EnumClass.OrderStateEnum.Packaging);
                Session["activeClass"] = "PackagingOrders";
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
        public ActionResult ShippiedOrders()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Orders = _orderManager.GetOrderListByStatus((int)EnumClass.OrderStateEnum.Shipped);
                Session["activeClass"] = "ShippiedOrders";
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }        
        }
        public ActionResult DeliveredOrders()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Orders = _orderManager.GetOrderListByStatus((int)EnumClass.OrderStateEnum.Delivered);
                Session["activeClass"] = "DeliveredOrders";
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
        public ActionResult ReceivedOrders()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Orders = _orderManager.GetOrderListByStatus((int)EnumClass.OrderStateEnum.Received);
                Session["activeClass"] = "ReceivedOrders";
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
        public ActionResult CanceledOrders()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Orders = _orderManager.GetOrderListByStatus((int)EnumClass.OrderStateEnum.Cancelled);
                Session["activeClass"] = "CancelOrder";
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
        public ActionResult OrderByLocation()
        {
            return View();
        }
    }
}