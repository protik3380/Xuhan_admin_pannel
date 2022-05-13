using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.View_Model
{
    public class ReturningCustomerVM
    {
        [JsonProperty("returningCustomer")]
        public int ReturningCustomer { get; set; }

        [JsonProperty("totalUser")]
        public int TotalUser { get; set; }

        [JsonProperty("totalCustomer")]
        public int TotalCustomer { get; set; }
    }
}