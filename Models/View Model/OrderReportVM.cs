using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.View_Model
{
    public class OrderReportVM
    {
        [JsonProperty("orderNo")]
        public string OrderNo { get; set; }

        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [JsonProperty("totalPrice")]
        public decimal TotalPrice { get; set; }

        [JsonProperty("totalProducts")]
        public string TotalProducts { get; set; }

        [JsonProperty("thana")]
        public string Thana { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("orderDate")]
        public DateTime OrderDate { get; set; }

        [JsonProperty("masterDepotName")]
        public string MasterDepotName { get; set; }
    }
}