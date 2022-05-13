using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Models.RequestDto;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Manager
{
    public class OrderManager
    {
        public  List<Order> GetOrderListByStatus(int statusId)
        {
            IEnumerable<Order> orders = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("order/getAllOrderByStateId/"+statusId);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Order>>();
                    readTask.Wait();
                    orders = readTask.Result;
                }
                else
                {
                    orders = new List<Order>();
                }
            }
            return orders.ToList();
        }

        public List<Order> GetOrderListByStatusIds(OrderSearchDto searchDto)
        {
            IEnumerable<Order> orders = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.PostAsJsonAsync("order/getAllOrderByOrderStatus", searchDto);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Order>>();
                    readTask.Wait();
                    orders = readTask.Result;
                }
                else
                {
                    orders = new List<Order>();
                }
            }
            return orders.ToList();
        }
    }



}