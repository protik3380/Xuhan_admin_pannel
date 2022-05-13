using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using xtbdEcommerceAdminPanel.Models;

namespace xtbdEcommerceAdminPanel.Utility
{
    public static class ApiUtility
    {
        public static CountOrder CountAllOrder()
        {
            CountOrder orders = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("status/state/countOrder");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CountOrder>();
                    readTask.Wait();
                    orders = readTask.Result;
                }
                else
                {
                    orders = new CountOrder();
                }
            }
            return orders;
        }
    }
}